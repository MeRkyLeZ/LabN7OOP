﻿using System;
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
        Repository repos = new Repository(10);   // Хранилище объектов
        int R = 50; // Размер фигуры


        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e) // Отрисовка формы
        {
            for (int i = 0; i < repos.getSize(); ++i)
            {
                if (!repos.isNull(i))
                {
                    repos.getObject(i).draw(e.Graphics);    // Рисуем объекты
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)  //Обработчик нажатия поля
        {
            bool check = false;  // Проверка нахождения объекта при нажатии
            int x, y;   // Координаты нажатой клавиши
            x = this.PointToClient(Cursor.Position).X - pictureBox1.Location.X;
            y = this.PointToClient(Cursor.Position).Y - pictureBox1.Location.Y;
            if ((Control.ModifierKeys == Keys.Control)) // Проверка нажатия Ctrl
            {
                for (int i = 0; i < repos.getSize(); ++i)
                {
                    if (!repos.isNull(i))
                    {
                        repos.getObject(i).setSelected(x, y);   // Проверка нажатия
                    }
                }
            }
            else
            {
                for (int i = 0; i < repos.getSize(); ++i)   // Убираем выделения
                {
                    if (!repos.isNull(i))
                        repos.getObject(i).unSelected();
                }

                for (int i = 0; i < repos.getSize(); ++i)
                {
                    if (!repos.isNull(i))
                    {
                        repos.getObject(i).setSelected(x, y);   // Проверка нажатия
                        if (repos.getObject(i).getSelected() == true)
                            check = true;
                    }
                }
                if (check == false) // Если не нашли объект
                {
                    CShapes fig;    // Создаем объект, в зависимости от меню
                    if (кругToolStripMenuItem.Checked)
                    {
                        fig = new CCircle(x, y, R);
                    }
                    else if (квадратToolStripMenuItem.Checked)
                    {
                        fig = new CSquare(x, y, R);
                    }
                    else if (треугольникToolStripMenuItem.Checked)
                    {
                        fig = new CTriangle(x, y, R);
                    }
                    else fig = null;
                    if (fig != null && fig.CheckIn(0, pictureBox1.Width, 0, pictureBox1.Height))
                    {
                        repos.addObject(fig);
                        unCheckedMenu();    // Снимаем выделения в меню
                    }
                }
            }
            pictureBox1.Refresh();	// Обновление формы
        }
        private void unCheckedMenu()    // Снимаем выделения в меню
        {
            кругToolStripMenuItem.Checked = false;
            квадратToolStripMenuItem.Checked = false;
            треугольникToolStripMenuItem.Checked = false;
        }
        private void кругToolStripMenuItem_Click(object sender, EventArgs e)    // Запоминаем объект для создания
        {
            unCheckedMenu();
            кругToolStripMenuItem.Checked = true;
        }

        private void квадратToolStripMenuItem_Click(object sender, EventArgs e) // Запоминаем объект для создания
        {
            unCheckedMenu();
            квадратToolStripMenuItem.Checked = true;
        }

        private void треугольникToolStripMenuItem_Click(object sender, EventArgs e) // Запоминаем объект для создания
        {
            unCheckedMenu();
            треугольникToolStripMenuItem.Checked = true;
        }

        private void увеличитьToolStripMenuItem_Click(object sender, EventArgs e)   // Увеличение объекта
        {
            for (int i = 0; i < repos.getSize(); ++i)
            {
                if (!repos.isNull(i))
                {
                    if (repos.getObject(i).getSelected())
                    {
                        if (repos.getObject(i).CheckIn(10, pictureBox1.Width - 10, 10, pictureBox1.Height - 10))  // Проверка выхода
                            repos.getObject(i).setSize(10); // Увеличиваем размер
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
                        repos.getObject(i).setSize(-10);    // Уменьшаем размер
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
                        if (repos.getObject(i).CheckIn(0, pictureBox1.Width, 10, pictureBox1.Height))   // Проверка выхода
                            repos.getObject(i).move(0, -10);    // Смещение
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
                        if (repos.getObject(i).CheckIn(0, pictureBox1.Width, 0, pictureBox1.Height - 10))   // Проверка выхода
                            repos.getObject(i).move(0, 10); // Смещение
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
                        if (repos.getObject(i).CheckIn(10, pictureBox1.Width, 0, pictureBox1.Height))   // Проверка выхода
                            repos.getObject(i).move(-10, 0);    // Смещение
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
                        if (repos.getObject(i).CheckIn(0, pictureBox1.Width - 10, 0, pictureBox1.Height))   // Проверка выхода
                            repos.getObject(i).move(10, 0); // Смещение
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
                    if (repos.getObject(i).getSelected())
                        repos.getObject(i).setColor(Color.Black);
            }
            pictureBox1.Refresh();	// Обновление формы
        }

        private void красныйToolStripMenuItem_Click(object sender, EventArgs e) // Смена цвета объекта
        {
            for (int i = 0; i < repos.getSize(); ++i)
            {
                if (!repos.isNull(i))
                    if (repos.getObject(i).getSelected())
                        repos.getObject(i).setColor(Color.Red);
            }
            pictureBox1.Refresh();	// Обновление формы
        }

        private void зеленыйToolStripMenuItem_Click(object sender, EventArgs e) // Смена цвета объекта
        {
            for (int i = 0; i < repos.getSize(); ++i)
            {
                if (!repos.isNull(i))
                    if (repos.getObject(i).getSelected())
                        repos.getObject(i).setColor(Color.Green);
            }
            pictureBox1.Refresh();	// Обновление формы
        }

        private void синийToolStripMenuItem_Click(object sender, EventArgs e)   // Смена цвета объекта
        {
            for (int i = 0; i < repos.getSize(); ++i)
            {
                if (!repos.isNull(i))
                    if (repos.getObject(i).getSelected())
                        repos.getObject(i).setColor(Color.Blue);
            }
            pictureBox1.Refresh();	// Обновление формы
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e) //Удаление объекта
        {
            for (int i = 0; i < repos.getSize(); ++i)
            {
                if (!repos.isNull(i))
                    if (repos.getObject(i).getSelected())
                        repos.delObject(i);
            }
            pictureBox1.Refresh();	// Обновление формы
        }

        private void создатьГруппуToolStripMenuItem_Click(object sender, EventArgs e)   // Создать группу
        {
            Group gr = new Group(1);    // Группа
            for (int i = 0; i < repos.getSize(); ++i)
            {
                if (!repos.isNull(i))
                    if (repos.getObject(i).getSelected())
                    {
                        gr.addShape(repos.getObject(i));    // Добавляем в группу
                        repos.delObject(i);
                    }
            }
            repos.addObject(gr);    // Добавляем в хранилище
        }

        private void разгруппироватьToolStripMenuItem_Click(object sender, EventArgs e) // Удалить группу
        {
            for (int i = 0; i < repos.getSize(); ++i)
            {
                if (!repos.isNull(i))
                    if (repos.getObject(i).getSelected())
                    {
                        if (repos.getObject(i) is Group)    // Если это группа
                        {
                            Group gr = (Group)repos.getObject(i);
                            for (int j = 0; j < gr.getCount(); ++j)
                            {
                                if (gr.getGroups()[j] is Group)
                                    gr.getGroups()[j].unSelected();
                                repos.addObject(gr.getGroups()[j]);
                            }

                            repos.delObject(i);
                        }
                        else
                        {
                            repos.getObject(i).unSelected();
                        }
                    }
            }
            pictureBox1.Refresh();	// Обновление формы
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)   // Загружаем из файла
        {
            repos.loadShapes("shapes.txt");
            pictureBox1.Refresh();	// Обновление формы
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)   // Сохраняем в файл
        {
            repos.saveShapes("shapes.txt");
            pictureBox1.Refresh();	// Обновление формы
        }
    }
}