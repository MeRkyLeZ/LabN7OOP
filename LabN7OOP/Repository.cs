using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LabN7OOP
{


    class Repository    // Хранилище
    {
        private CShapes[] arr;   // Массив элементов
        private int size;   // Размер массива
        private int count;  // Количество элементов

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
            if (pos == size - 1)
            {
                size++;
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

    }
}
