using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabN7OOP
{
    public abstract class CShapes
    {
        public abstract void move(int dx, int dy);  // Смещение объекта
        public abstract void draw(Graphics g);  // Зарисовка объекта
        public abstract void setSize(int dR);   // Установка размера
        public abstract bool getSelected(); // Проверка выбора
        public abstract void setSelected(int x, int y); // Вычисление попадания
        public abstract void setSelected();    // Установка выбора
        public abstract void unSelected();  // Снятие выбора
        public abstract void setColor(Color col);   // Установка цвета
        public abstract bool CheckIn(int X1, int X2, int Y1, int Y2);   // Проверка выхода за поле рисования
        public abstract void save(StreamWriter stream); // Сохранение
        public abstract void load(StreamReader stream); // Загрузка
    }

    public class Group : CShapes    // Группа
    {
        private int size;   // Размер группы
        private int count;  // Коль-во элементов
        private bool selected;
        private CShapes[] group;

        public Group(int size)
        {
            this.size = size;
            count = 0;
            group = new CShapes[size];
            selected = true;
        }

        ~Group()
        {
            for (int i = 0; i < size; ++i)
            {
                group[i] = null;
            }
        }

        public void addShape(CShapes shape) // Добавление элемента
        {
            if (count >= size)
            {
                size = count + 1;
                CShapes[] tmp = new CShapes[size];
                for (int i = 0; i < size - 1; ++i)
                {
                    tmp[i] = group[i];
                }
                group = tmp;
            }
            group[count++] = shape;

        }

        public override void draw(Graphics g)   // Зарисовка
        {
            for (int i = 0; i < size; ++i)
            {
                group[i].draw(g);
            }
        }

        public override bool getSelected()
        {
            return selected;
        }

        public override void move(int dx, int dy)   // Перемещение
        {
            for (int i = 0; i < size; ++i)
            {
                group[i].move(dx, dy);
            }
        }

        public override void setSelected(int x, int y)
        {
            for (int i = 0; i < size; ++i)
            {
                group[i].setSelected(x, y);
                if (group[i].getSelected())
                {
                    selected = true;
                }
            }
            if (selected)
            {
                for (int i = 0; i < size; ++i)
                {
                    group[i].setSelected();
                }
            }
        }


        public override void setSelected()
        {
            selected = true;
            for (int i = 0; i < size; ++i)
            {
                group[i].setSelected();
            }
        }
        public int getCount()
        {
            return count;
        }

        public CShapes[] getGroups()
        {
            return group;
        }

        public override void setSize(int dR)    // Установка размера
        {
            for (int i = 0; i < size; ++i)
            {
                group[i].setSize(dR);
            }
        }

        public override bool CheckIn(int X1, int X2, int Y1, int Y2)
        {
            bool check = true;
            for (int i = 0; i < size; ++i)
            {
                if (group[i] is Figure)
                {
                    Figure c = (Figure)group[i];
                    if (!(c.getX() + c.getR() < (X1 + X2) && c.getX() - c.getR() > X1 && c.getY() + c.getR() < (Y1 + Y2) && c.getY() - c.getR() > Y1))
                        check = false;
                }
                else if (group[i].CheckIn(X1, X2, Y1, Y2) == false)
                    check = false;
            }
            return check;
        }

        public override void unSelected()
        {
            selected = false;
            for (int i = 0; i < size; ++i)
            {
                group[i].unSelected();
            }
        }

        public override void setColor(Color col)
        {
            for (int i = 0; i < size; ++i)
            {
                group[i].setColor(col);
            }
        }

        public override void save(StreamWriter stream)
        {
            stream.WriteLine("Group");
            stream.WriteLine(count);
            for (int i = 0; i < size; ++i)
            {
                group[i].save(stream);
            }
        }

        public override void load(StreamReader stream)
        {
            int imax = Convert.ToInt32(stream.ReadLine());
            for (int i = 0; i < imax; ++i)
            {
                CShapes cs = null;
                switch (stream.ReadLine())
                {
                    case "CCircle":
                        cs = new CCircle(0, 0, 0);
                        break;
                    case "CSquare":
                        cs = new CSquare(0, 0, 0);
                        break;
                    case "CTriangle":
                        cs = new CTriangle(0, 0, 0);
                        break;
                    case "Group":
                        cs = new Group(1);
                        break;
                }
                addShape(cs);
                group[i].load(stream);
            }
        }
    }

    public abstract class Figure : CShapes
    {
        public abstract int GetDistance(int x, int y);   // Вычисление дистанции между точками
        public abstract int getR(); // Получение размера
        public abstract int getX(); // Получение X
        public abstract int getY(); // Получение Y
    }

    public class CCircle : Figure   // Объект
    {
        private int x, y, R;
        private bool selected;
        private Color col;
        public CCircle()    // Конструктор
        {
            x = 0;
            y = 0;
            R = 0;
            selected = true;
            col = Color.Black;
        }
        public CCircle(int x, int y, int R) // Конструктор
        {
            this.x = x;
            this.y = y;
            this.R = R;
            selected = true;
            col = Color.Black;
        }
        ~CCircle()  // Деструктор
        {
        }

        public override void move(int dx, int dy)
        {
            x += dx;
            y += dy;
        }

        public override void draw(Graphics g)
        {
            Pen pen = new Pen(col);    // Кисть
            Brush brush = new SolidBrush(Color.Black); // Заливка
            if (selected == false)
            {
                g.DrawEllipse(pen, x - R, y - R, R * 2, R * 2);  // Рисуем элемент
            }
            else
            {

                g.FillEllipse(brush, x - R, y - R, R * 2, R * 2);    // Заливаем элемент
            }
        }

        public override bool getSelected()
        {
            return selected;
        }

        public override void setColor(Color col)
        {
            this.col = col;
        }

        public override void setSelected(int x, int y)
        {
            if (GetDistance(x, y) <= (int)Math.Pow(getR(), 2))
            {
                selected = true;
            }
        }

        public override void setSelected()
        {
            selected = true;
        }

        public override bool CheckIn(int X1, int X2, int Y1, int Y2)
        {
            return (x + R < (X1 + X2) && x - R > X1 && y + R < (Y1 + Y2) && y - R > Y1);
        }

        public override int GetDistance(int x, int y)
        {
            return ((int)Math.Pow((this.x - x), 2) + (int)Math.Pow((this.y - y), 2));
        }

        public override int getR()
        {
            return R;
        }

        public override void setSize(int dR)
        {
            if (R + dR > 0)
                R += dR;
        }

        public override int getX()
        {
            return x;
        }

        public override int getY()
        {
            return y;
        }

        public override void unSelected()
        {
            selected = false;
        }

        public override void save(StreamWriter stream)
        {
            stream.WriteLine("CCircle");
            stream.WriteLine(x);
            stream.WriteLine(y);
            stream.WriteLine(R);
            stream.WriteLine(col.R + "," + col.G + "," + col.B);
        }

        public override void load(StreamReader stream)
        {
            x = Convert.ToInt32(stream.ReadLine());
            y = Convert.ToInt32(stream.ReadLine());
            R = Convert.ToInt32(stream.ReadLine());
            String[] str = stream.ReadLine().Split(','); ;
            col = Color.FromArgb(Convert.ToInt32(str[0]), Convert.ToInt32(str[1]), Convert.ToInt32(str[2]));
            selected = false;
        }
    }

    class CSquare : Figure   // Объект
    {
        private int x, y, R;
        private bool selected;
        private Color col;
        public CSquare()    // Конструктор
        {
            x = 0;
            y = 0;
            R = 0;
            selected = true;
            col = Color.Black;
        }
        public CSquare(int x, int y, int R) // Конструктор
        {
            this.x = x;
            this.y = y;
            this.R = R;
            selected = true;
            col = Color.Black;
        }

        ~CSquare()  // Деструктор
        {

        }

        public override void move(int dx, int dy)
        {
            x += dx;
            y += dy;
        }

        public override void draw(Graphics g)
        {
            Pen pen = new Pen(col);    // Кисть
            Brush brush = new SolidBrush(Color.Black); // Заливка
            if (selected == false)
            {
                g.DrawRectangle(pen, x - R, y - R, R * 2, R * 2);  // Рисуем элемент
            }
            else
            {
                g.FillRectangle(brush, x - R, y - R, R * 2, R * 2);    // Заливаем элемент
            }
        }

        public override bool getSelected()
        {
            return selected;
        }

        public override void setColor(Color col)
        {
            this.col = col;
        }

        public override void setSelected(int x, int y)
        {
            if (GetDistance(x, y) <= (int)Math.Pow(getR(), 2))
            {
                selected = true;
            }
        }

        public override void setSelected()
        {
            selected = true;
        }

        public override bool CheckIn(int X1, int X2, int Y1, int Y2)
        {

            return (x + R < (X1 + X2) && x - R > X1 && y + R < (Y1 + Y2) && y - R > Y1);

        }

        public override int GetDistance(int x, int y)
        {
            return ((int)Math.Pow((this.x - x), 2) + (int)Math.Pow((this.y - y), 2));
        }
        public override int getR()
        {
            return R;
        }

        public override void setSize(int dR)
        {
            if (R + dR > 0)
                R += dR;
        }

        public override int getX()
        {
            return x;
        }

        public override int getY()
        {
            return y;
        }

        public override void unSelected()
        {
            selected = false;
        }

        public override void save(StreamWriter stream)
        {
            stream.WriteLine("CSquare");
            stream.WriteLine(x);
            stream.WriteLine(y);
            stream.WriteLine(R);
            stream.WriteLine(col.R + "," + col.G + "," + col.B);
        }

        public override void load(StreamReader stream)
        {
            x = Convert.ToInt32(stream.ReadLine());
            y = Convert.ToInt32(stream.ReadLine());
            R = Convert.ToInt32(stream.ReadLine());
            String[] str = stream.ReadLine().Split(','); ;
            col = Color.FromArgb(Convert.ToInt32(str[0]), Convert.ToInt32(str[1]), Convert.ToInt32(str[2]));
            selected = false;
        }
    }

    class CTriangle : Figure    // Объект
    {
        private int x, y, R;
        private bool selected;
        private Color col;
        public CTriangle()  // Конструктор
        {
            x = 0;
            y = 0;
            R = 0;
            selected = true;
            col = Color.Black;
        }
        public CTriangle(int x, int y, int R)   // Конструктор
        {
            this.x = x;
            this.y = y;
            this.R = R;
            selected = true;
            col = Color.Black;
        }
        ~CTriangle()    // Деструктор
        {

        }

        public override void move(int dx, int dy)
        {
            x += dx;
            y += dy;
        }

        public override void draw(Graphics g)
        {
            Pen pen = new Pen(col);    // Кисть
            Brush brush = new SolidBrush(Color.Black); // Заливка
            Point p1 = new Point(x, y - R);
            Point p2 = new Point(x - R, y + R);
            Point p3 = new Point(x + R, y + R);
            Point[] p = new Point[3];   // Треугольник
            p[0] = p1;
            p[1] = p2;
            p[2] = p3;
            if (selected == false)
            {
                g.DrawPolygon(pen, p); // Рисуем элемент
            }
            else
            {
                g.FillPolygon(brush, p);   // Заливаем элемент
            }
        }

        public override bool getSelected()
        {
            return selected;
        }

        public override void setColor(Color col)
        {
            this.col = col;
        }

        public override void setSelected(int x, int y)
        {
            if (GetDistance(x, y) <= (int)Math.Pow(getR(), 2))
            {
                selected = true;
            }
        }

        public override void setSelected()
        {
            selected = true;
        }
        public override bool CheckIn(int X1, int X2, int Y1, int Y2)
        {
            return (x + R < (X1 + X2) && x - R > X1 && y + R < (Y1 + Y2) && y - R > Y1);
        }

        public override int GetDistance(int x, int y)
        {
            return ((int)Math.Pow((this.x - x), 2) + (int)Math.Pow((this.y - y), 2));
        }
        public override int getR()
        {
            return R;
        }

        public override void setSize(int dR)
        {
            if (R + dR > 0)
                R += dR;
        }

        public override int getX()
        {
            return x;
        }

        public override int getY()
        {
            return y;
        }

        public override void unSelected()
        {
            selected = false;
        }

        public override void save(StreamWriter stream)
        {
            stream.WriteLine("CTriangle");
            stream.WriteLine(x);
            stream.WriteLine(y);
            stream.WriteLine(R);
            stream.WriteLine(col.R + "," + col.G + "," + col.B);
        }

        public override void load(StreamReader stream)
        {
            x = Convert.ToInt32(stream.ReadLine());
            y = Convert.ToInt32(stream.ReadLine());
            R = Convert.ToInt32(stream.ReadLine());
            String[] str = stream.ReadLine().Split(','); ;
            col = Color.FromArgb(Convert.ToInt32(str[0]), Convert.ToInt32(str[1]), Convert.ToInt32(str[2]));
            selected = false;
        }
    }
}
