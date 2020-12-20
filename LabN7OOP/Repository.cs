using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LabN7OOP
{
    public class Data
    {
        protected CShapes[] arr;    // Массив элементов
        protected int size;   // Размер массива
        protected int count;  // Количество элементов
        public virtual CShapes createShape(string c)
        {
            return null;
        }

        public void loadShapes(string filename) // Загрузка фигур
        {
            arr = null;
            arr = new CShapes[10];
            StreamReader sr;
            using (sr = new StreamReader(filename)) // Открываем файл
            {
                count = Convert.ToInt32(sr.ReadLine()); // Считываем кол-во файлов
                if (count > size)   // Проверяем размер
                {
                    size = count + 1;
                    CShapes[] tmp = new CShapes[size];
                    arr = tmp;
                }
                for (int i = 0; i < count; ++i)
                {
                    arr[i] = createShape(sr.ReadLine());    // Создаем фигуру

                    if (arr[i] != null)
                        arr[i].load(sr);    // Загружаем фигуру
                }
            }
            sr.Close(); // Закрываем файл
        }

        public void saveShapes(string filename) // Сохранение фигур
        {
            StreamWriter sw;
            using (sw = new StreamWriter(filename)) // Открываем файл
            {
                sw.WriteLine(count);    // Записываем кол-во файлов
                for (int i = 0; i < size; ++i)
                {
                    if (arr[i] != null)
                        arr[i].save(sw);    // Сохраняем фигуру
                }
            }
            sw.Close(); // Закрываем файл
        }
    }


    class Repository : Data    // Хранилище
    {
        public Repository() // Конструктор
        {
            size = 0;
            count = 0;
            arr = new CShapes[size];
        }
        public Repository(int size) // Конструктор
        {
            this.size = size;
            count = 0;
            arr = new CShapes[size];
        }
        ~Repository()   // Деструктор
        {
            for (int i = 0; i < size; ++i)
            {
                if (!isNull(i))
                    arr[i] = null;
            }
        }
        public void delObject(int pos)  // Удаление объекта
        {
            arr[pos] = null;
            count--;
        }
        public CShapes addObject(CShapes point) // Добавление элемента
        {
            int pos = 0;
            while (!isNull(pos) && pos < size)
            {
                pos++;
            }
            if (pos >= size - 1)
            {
                size = pos + 1;
                CShapes[] tmp = new CShapes[size];
                for (int i = 0; i < size - 1; ++i)
                {
                    tmp[i] = arr[i];
                }
                arr = tmp;
            }
            arr[pos] = point;
            count++;
            return arr[pos];
        }
        public void setObject(int pos, CShapes point) // Изменение элемента
        {
            if (pos >= size)
            {
                int oldsize = size;
                size = pos + 1;
                CShapes[] tmp = new CShapes[size];
                for (int i = 0; i < size - 1; ++i)
                {
                    tmp[i] = arr[i];
                }
                arr = tmp;
                count++;
            }
            arr[pos] = point;
        }
        public CShapes getObject(int pos)    // Получение элемента
        {
            return arr[pos];
        }
        public int getCount()   // Получение количества объектов
        {
            return count;
        }
        public int getSize()    // Получение размера хранилища
        {
            return size;
        }
        public bool isNull(int pos) // Проверка наличия
        {
            if (arr[pos] == null)
                return true;
            return false;
        }

        public override CShapes createShape(string c)   // Создаем фигуру
        {
            CShapes cs = null;
            switch (c)
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
            return cs;
        }
    }
}
