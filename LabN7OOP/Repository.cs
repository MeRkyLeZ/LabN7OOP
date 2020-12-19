using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LabN7OOP
{


    class Repository    // Хранилище
    {
        private Figure[] arr;   // Массив элементов
        private int size;   // Размер массива
        private int count;  // Количество элементов

        public Repository() // Конструктор
        {
            size = 0;
            count = 0;
            arr = new Figure[size];
        }
        public Repository(int size) // Конструктор
        {
            this.size = size;
            count = 0;
            arr = new Figure[size];
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
        public void addObject(Figure point) // Добавление элемента
        { 
            int pos = 0;
            while (!isNull(pos) && pos < size)
            {
                pos++;
            }
            if (pos == size)
            {
                size++;
                Figure[] tmp = new Figure[size];
                for (int i = 0; i < size - 1; ++i)
                {
                    tmp[i] = arr[i];
                }
                arr = tmp;
            }
            arr[pos] = point;
            count++;
        }
        public void setObject(int pos,Figure point) // Изменение элемента
        {
            if (pos >= size)
            {
                int oldsize = size;
                size = pos + 1;
                Figure[] tmp = new Figure[size];
                for (int i = 0; i < size - 1; ++i)
                {
                    tmp[i] = arr[i];
                }
                arr = tmp;
                count++;
            }
            arr[pos] = point;
        }
        public Figure getObject(int pos)    // Получение элемента
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

    }
}
