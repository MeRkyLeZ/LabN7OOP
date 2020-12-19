using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabN7OOP
{
    public class CShapes
    {
    }


    public class Figure // Фигура
    {
        protected int x, y, R;
        protected bool selected;
        Color col;
        public Figure()
        {
            x = 0;
            y = 0;
            R = 0;
            selected = true;
            col = Color.Black;
        }
        Figure(int x, int y)    // Конструктор
        {
            this.x = x;
            this.y = y;
            selected = true;
            R = 0;
            col = Color.Black;
        }
        ~Figure()   // Деструктор
        {

        }
        public int getX()
        {
            return x;
        }
        public int getY()
        {
            return y;
        }
        public void setX(int x)
        {
            this.x = x;
        }
        public void setY(int y)
        {
            this.y = y;
        }
        public void setSelected(bool selected)
        {
            this.selected = selected;
        }
        public bool getSelected()
        {
            return selected;
        }
        public Color getColor()
        {
            return col;
        }
        public void setColor(Color col)
        {
            this.col = col;
        }
        public int getR()
        {
            return R;
        }
        public void setR(int R)
        {
            this.R = R;
        }
    }

    public class CCircle : Figure    // Объект
    {
        public CCircle()    // Конструктор
        {
            x = 0;
            y = 0;
            R = 0;
            selected = true;
        }
        public CCircle(int x, int y, int R) // Конструктор
        {
            this.x = x;
            this.y = y;
            this.R = R;
            selected = true;
        }
        ~CCircle()  // Деструктор
        {
        }
    }

    class CSquare : Figure   // Объект
    {
        public CSquare()    // Конструктор
        {
            x = 0;
            y = 0;
            R = 0;
            selected = true;
        }
        public CSquare(int x, int y, int R) // Конструктор
        {
            this.x = x;
            this.y = y;
            this.R = R;
            selected = true;
        }

        ~CSquare()  // Деструктор
        {

        }
    }

    class CTriangle : Figure     // Объект
    {
        public CTriangle()  // Конструктор
        {
            x = 0;
            y = 0;
            R = 0;
            selected = true;
        }
        public CTriangle(int x, int y, int R)   // Конструктор
        {
            this.x = x;
            this.y = y;
            this.R = R;
            selected = true;
        }
        ~CTriangle()    // Деструктор
        {

        }
    }
}
