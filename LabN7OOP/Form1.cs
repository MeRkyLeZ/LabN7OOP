using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabN7OOP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Repository repos = new Repository(10);   // Хранилище объектов
        int R = 50; // Размер фигуры
        Figure fig;  // Фигура для создания

        private int GetDistance(int x0, int x, int y0, int y)   // Вычисление дистанции между точками
        {
            return ((int)Math.Pow((x0 - x), 2) + (int)Math.Pow((y0 - y), 2));
        }
        private bool CheckIn(int x, int y, int R)   // Проверка выхода за поле рисования
        {
            return (x + R < (pictureBox1.Location.X + pictureBox1.Width) && x - R > pictureBox1.Location.X && y + R < (pictureBox1.Location.Y + pictureBox1.Height) && y - R > pictureBox1.Location.Y);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e) // Отрисовка формы
        {
            Pen pen = new Pen(Color.Black);    // Кисть
            Brush brush = new SolidBrush(Color.Black); // Заливка
            for (int i = 0; i < repos.getSize(); ++i)
            {
                if (!repos.isNull(i))
                {
                    pen.Color = repos.getObject(i).getColor();
                    if (repos.getObject(i) is CCircle)
                    {
                        CCircle c = (CCircle)repos.getObject(i);
                        if (repos.getObject(i).getSelected() == false)
                        {
                            e.Graphics.DrawEllipse(pen, c.getX() - c.getR(), c.getY() - c.getR(), c.getR() * 2, c.getR() * 2);  // Рисуем элемент
                        }
                        else
                        {

                            e.Graphics.FillEllipse(brush, c.getX() - c.getR(), c.getY() - c.getR(), c.getR() * 2, c.getR() * 2);    // Заливаем элемент
                        }
                    }

                    else if (repos.getObject(i) is CSquare)
                    {
                        CSquare c = (CSquare)repos.getObject(i);
                        if (repos.getObject(i).getSelected() == false)
                        {
                            e.Graphics.DrawRectangle(pen, c.getX() - c.getR(), c.getY() - c.getR(), c.getR() * 2, c.getR() * 2);    // Рисуем элемент
                        }
                        else
                        {

                            e.Graphics.FillRectangle(brush, c.getX() - c.getR(), c.getY() - c.getR(), c.getR() * 2, c.getR() * 2);  // Заливаем элемент
                        }

                    }
                    else if (repos.getObject(i) is CTriangle)
                    {

                        CTriangle c = (CTriangle)repos.getObject(i);
                        Point p1 = new Point(c.getX(), c.getY() - c.getR());
                        Point p2 = new Point(c.getX() - c.getR(), c.getY() + c.getR());
                        Point p3 = new Point(c.getX() + c.getR(), c.getY() + c.getR());
                        Point[] p = new Point[3];   // Треугольник
                        p[0] = p1;
                        p[1] = p2;
                        p[2] = p3;
                        if (repos.getObject(i).getSelected() == false)
                        {
                            e.Graphics.DrawPolygon(pen, p); // Рисуем элемент
                        }
                        else
                        {
                            e.Graphics.FillPolygon(brush, p);   // Заливаем элемент
                        }
                    }

                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)  //Обработчик нажатия поля
        {
            int check = 0;  // Проверка нахождения объекта при нажатии
            int x, y;
            if ((Control.ModifierKeys == Keys.Control)) // Проверка нажатия Ctrl
            {
                x = this.PointToClient(Cursor.Position).X - pictureBox1.Location.X;
                y = this.PointToClient(Cursor.Position).Y - pictureBox1.Location.Y;
                for (int i = 0; i < repos.getSize(); ++i)
                {
                    if (!repos.isNull(i))
                    {
                        if (GetDistance(repos.getObject(i).getX(), x, repos.getObject(i).getY(), y) <= (int)Math.Pow(repos.getObject(i).getR(), 2))
                        {
                            if (repos.getObject(i).getSelected() == false)
                                check = 1;
                        }
                    }
                    if (check > 0) break;
                }
                if (check != 0)
                    for (int i = 0; i < repos.getSize(); ++i)
                    {
                        if (!repos.isNull(i))
                        {
                            if (GetDistance(repos.getObject(i).getX(), x, repos.getObject(i).getY(), y) <= (int)Math.Pow(repos.getObject(i).getR(), 2))
                            {
                                repos.getObject(i).setSelected(true);
                            }
                        }
                    }

            }
            else
            {
                for (int i = 0; i < repos.getSize(); ++i)
                {
                    if (!repos.isNull(i))
                        repos.getObject(i).setSelected(false);
                }
                x = this.PointToClient(Cursor.Position).X - pictureBox1.Location.X;
                y = this.PointToClient(Cursor.Position).Y - pictureBox1.Location.Y;
                for (int i = 0; i < repos.getSize(); ++i)
                {
                    if (!repos.isNull(i))
                    {
                        if (GetDistance(repos.getObject(i).getX(), x, repos.getObject(i).getY(), y) <= (int)Math.Pow(repos.getObject(i).getR(), 2))
                        {
                            check = 1;
                            repos.getObject(i).setSelected(true);
                        }
                    }
                }
                if (check == 0) // Если не нашли объект
                {
                    if (fig != null)
                    {
                        if (fig is CCircle)
                        {
                            if (CheckIn(x, y, R))
                            {
                                repos.addObject(new CCircle(x, y, R));
                                fig = null;

                            }
                        }
                        else if (fig is CSquare)
                        {
                            if (CheckIn(x, y, R))
                            {
                                repos.addObject(new CSquare(x, y, R));
                                fig = null;
                            }
                        }
                        else if (fig is CTriangle)
                        {
                            if (CheckIn(x, y, R))
                            {
                                repos.addObject(new CTriangle(x, y, R));
                                fig = null;
                            }
                        }
                    }

                }
            }
            pictureBox1.Refresh();	// Обновление формы
        }

        private void кругToolStripMenuItem_Click(object sender, EventArgs e)    // Запоминаем объект для создания
        {
            fig = null;
            fig = new CCircle();
        }

        private void квадратToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fig = null;
            fig = new CSquare();
        }

        private void треугольникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fig = null;
            fig = new CTriangle();
        }

        private void увеличитьToolStripMenuItem_Click(object sender, EventArgs e)   // Увеличение объекта
        {
            for (int i = 0; i < repos.getSize(); ++i)
            {
                if (!repos.isNull(i))
                {
                    if (repos.getObject(i).getSelected())
                    {
                        if (CheckIn(repos.getObject(i).getX(), repos.getObject(i).getY(), repos.getObject(i).getR() + 10))  // Проверка выхода
                            repos.getObject(i).setR(repos.getObject(i).getR() + 10);
                    }
                }
            }
            pictureBox1.Refresh();	// Обновление формы
        }
        private void уменьшитьToolStripMenuItem_Click(object sender, EventArgs e)   // Уменьшение объекта
        {
            for (int i = 0; i < repos.getSize(); ++i)
            {
                if (!repos.isNull(i))
                {
                    if (repos.getObject(i).getSelected())
                    {
                        if (repos.getObject(i).getR() - 10 > 0)
                            repos.getObject(i).setR(repos.getObject(i).getR() - 10);
                    }
                }
            }
            pictureBox1.Refresh();	// Обновление формы
        }

        private void вверхToolStripMenuItem_Click(object sender, EventArgs e)   // Смещение объекта
        {
            for (int i = 0; i < repos.getSize(); ++i)
            {
                if (!repos.isNull(i))
                {
                    if (repos.getObject(i).getSelected())
                    {
                        if (CheckIn(repos.getObject(i).getX(), repos.getObject(i).getY() - 10, repos.getObject(i).getR()))  // Проверка выхода
                            repos.getObject(i).setY(repos.getObject(i).getY() - 10);
                    }
                }
            }
            pictureBox1.Refresh();	// Обновление формы
        }

        private void внизToolStripMenuItem_Click(object sender, EventArgs e)    // Смещение объекта
        {
            for (int i = 0; i < repos.getSize(); ++i)
            {
                if (!repos.isNull(i))
                {
                    if (repos.getObject(i).getSelected())
                    {
                        if (CheckIn(repos.getObject(i).getX(), repos.getObject(i).getY() + 10, repos.getObject(i).getR()))  // Проверка выхода
                            repos.getObject(i).setY(repos.getObject(i).getY() + 10);
                    }
                }
            }
            pictureBox1.Refresh();	// Обновление формы
        }

        private void влевоToolStripMenuItem_Click(object sender, EventArgs e)   // Смещение объекта
        {
            for (int i = 0; i < repos.getSize(); ++i)
            {
                if (!repos.isNull(i))
                {
                    if (repos.getObject(i).getSelected())
                    {
                        if (CheckIn(repos.getObject(i).getX() - 10, repos.getObject(i).getY(), repos.getObject(i).getR()))  // Проверка выхода
                            repos.getObject(i).setX(repos.getObject(i).getX() - 10);
                    }
                }
            }
            pictureBox1.Refresh();	// Обновление формы
        }

        private void вправоToolStripMenuItem_Click(object sender, EventArgs e)  // Смещение объекта
        {
            for (int i = 0; i < repos.getSize(); ++i)
            {
                if (!repos.isNull(i))
                {
                    if (repos.getObject(i).getSelected())
                    {
                        if (CheckIn(repos.getObject(i).getX() + 10, repos.getObject(i).getY(), repos.getObject(i).getR()))  // Проверка выхода
                            repos.getObject(i).setX(repos.getObject(i).getX() + 10);
                    }
                }
            }
            pictureBox1.Refresh();	// Обновление формы
        }

        private void черныйToolStripMenuItem_Click(object sender, EventArgs e)  // Смена цвета объекта
        {
            for (int i = 0; i < repos.getSize(); ++i)
            {
                if (!repos.isNull(i))
                    if (repos.getObject(i).getSelected() == true)
                        repos.getObject(i).setColor(Color.Black);
            }
            pictureBox1.Refresh();	// Обновление формы
        }

        private void красныйToolStripMenuItem_Click(object sender, EventArgs e) // Смена цвета объекта
        {
            for (int i = 0; i < repos.getSize(); ++i)
            {
                if (!repos.isNull(i))
                    if (repos.getObject(i).getSelected() == true)
                        repos.getObject(i).setColor(Color.Red);
            }
            pictureBox1.Refresh();	// Обновление формы
        }

        private void зеленыйToolStripMenuItem_Click(object sender, EventArgs e) // Смена цвета объекта
        {
            for (int i = 0; i < repos.getSize(); ++i)
            {
                if (!repos.isNull(i))
                    if (repos.getObject(i).getSelected() == true)
                        repos.getObject(i).setColor(Color.Green);
            }
            pictureBox1.Refresh();	// Обновление формы
        }

        private void синийToolStripMenuItem_Click(object sender, EventArgs e)   // Смена цвета объекта
        {
            for (int i = 0; i < repos.getSize(); ++i)
            {
                if (!repos.isNull(i))
                    if (repos.getObject(i).getSelected() == true)
                        repos.getObject(i).setColor(Color.Blue);
            }
            pictureBox1.Refresh();	// Обновление формы
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e) //Удаление объекта
        {
            for (int i = 0; i < repos.getSize(); ++i)
            {
                if (!repos.isNull(i))
                    if (repos.getObject(i).getSelected() == true)
                        repos.delObject(i);
            }
            pictureBox1.Refresh();	// Обновление формы
        }
    }
}