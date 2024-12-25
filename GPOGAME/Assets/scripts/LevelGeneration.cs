using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class LevelGrid 
{
    public int[,] levelGrid; // Массив для хранения сетки комнат уровня
    public Point startRoom;   // Координаты стартовой комнаты
    public int roomCount;     // Текущее количество комнат на уровне

    public LevelGrid(int levelnumber, int roomnumber, int seed )
    {

        LevelGenerate(levelnumber, roomnumber, seed);

    }

    // Метод вызывается при загрузке формы
    private void LevelGenerate(int levelNumber, int approximateRoomCount, int seed)
    {
      

        // Генерация уровня
        levelGrid = GenerateLevel(levelNumber, approximateRoomCount, seed);

        // Отображение сетки на форме
       

        // Вывод координат стартовой комнаты в консоль
        Console.WriteLine($"Start Room Coordinates: ({startRoom.X}, {startRoom.Y})");
    }

    // Метод для генерации уровня с заданными параметрами
    private int[,] GenerateLevel(int levelNumber, int approximateRoomCount, int seed)
    {
        int gridSize = (int)Math.Ceiling(Math.Sqrt(approximateRoomCount)) * 3; // Размер сетки с запасом
        levelGrid = new int[gridSize, gridSize]; // Инициализация пустой сетки

        // Заполнение всех ячеек как пустых (-1)
        for (int x = 0; x < gridSize; x++)
            for (int y = 0; y < gridSize; y++)
                levelGrid[x, y] = -1;

        // Установка стартовой комнаты в центре сетки
        startRoom = new Point(gridSize / 2, gridSize / 2);
        levelGrid[startRoom.X, startRoom.Y] = 0; // 0 обозначает обычную комнату
        roomCount = 1;

        // Создаем генератор случайных чисел на основе номера уровня и ключа генерации
        System.Random random = new System.Random(levelNumber * seed);

        // Генерация комнат, связанных со стартовой
        GenerateConnectedRooms(levelGrid, startRoom, gridSize, random, approximateRoomCount);

        // Размещение специальных комнат
        PlaceSpecialRooms(levelGrid, random, gridSize);

        return levelGrid;
    }

    // Метод для генерации комнат
    private void GenerateConnectedRooms(int[,] grid, Point start, int gridSize, System.Random random, int maxRooms)
    {
        Queue<Point> roomQueue = new Queue<Point>();
        roomQueue.Enqueue(start);
        ///////
        Queue<Point> hallQueue = new Queue<Point>(); // Комнаты для последующего их разветвления
                                                     ///////
        grid[start.X, start.Y] = 0; // Стартовая комната
        int roomCount = 1;

        int[] dx = { -1, 1, 0, 0 }; // Смещения по x для соседей
        int[] dy = { 0, 0, -1, 1 }; // Смещения по y для соседей

        List<int> usedDirections;

        // Пока не достигнуто максимальное количество комнат
        while (roomQueue.Count > 0 && roomCount < maxRooms)
        {
            Point currentRoom = roomQueue.Dequeue();

            usedDirections = new List<int>();

            //Добавление предыдущей комнаты
            usedDirections.Add((grid[currentRoom.X, currentRoom.Y] / 10) - 2);

            // Перемешиваем направления для случайности
            int[] directions = { 0, 1, 2, 3 };
            ShuffleArray(directions, random);

            // Пытаемся создать комнаты в каждом направлении
            for (int i = 0; i < 4 && roomCount < maxRooms; i++)
            {
                int nx = currentRoom.X + dx[directions[i]];
                int ny = currentRoom.Y + dy[directions[i]];

                // Проверка на границы
                if (nx >= 0 && nx < gridSize && ny >= 0 && ny < gridSize && grid[nx, ny] == -1)
                {
                    // Вероятность пустоты меньше для центральных комнат и выше для внешних
                    double distanceFactor = Math.Sqrt(Math.Pow(nx - start.X, 2) + Math.Pow(ny - start.Y, 2)) / (gridSize / 2.0);
                    double emptyChance = 0.2 + distanceFactor * 0.4; // Пустота больше по краям

                    if (random.NextDouble() < emptyChance)
                    {
                        grid[nx, ny] = -1; // Пустота
                    }
                    else
                    {
                        grid[nx, ny] = directions[i] switch
                        {
                            0 => 30,
                            1 => 20,
                            2 => 50,
                            3 => 40

                        };
                        ////////							
                        if (random.NextDouble() < 0.25)
                        {
                            hallQueue.Enqueue(new Point(nx, ny));
                        }
                        ////////
                        usedDirections.Add(directions[i]);
                        roomQueue.Enqueue(new Point(nx, ny));
                        roomCount++;
                    }
                }
            }
            //Индекс создаваемой комнаты
            grid[currentRoom.X, currentRoom.Y] = RoomChoise(usedDirections);

        }
        foreach (var room in hallQueue)
        {
            usedDirections = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                int nx = room.X + dx[i];
                int ny = room.Y + dy[i];
                if (grid[nx, ny] != -1)
                {
                    usedDirections.Add(i);
                    //Добавление ответной части у другой комнаты
                    switch (i)
                    {
                        case 0:
                            if ((grid[nx, ny] / 10) % 3 != 0)
                                grid[nx, ny] *= 3;
                            break;
                        case 1:
                            if ((grid[nx, ny] / 10) % 2 != 0 || ((grid[nx, ny] / 10) % 4 == 0 && (grid[nx, ny] / 10) % 8 != 0))
                                grid[nx, ny] *= 2;
                            break;
                        case 2:
                            if ((grid[nx, ny] / 10) % 5 != 0)
                                grid[nx, ny] *= 5;
                            break;
                        case 3:
                            if ((grid[nx, ny] / 10) % 4 != 0)
                                grid[nx, ny] *= 4;
                            break;
                    }

                }

            }
            grid[room.X, room.Y] = RoomChoise(usedDirections);
        }
    }

    private int RoomChoise(List<int> directions)
    {
        int roomIndex = 1;
        foreach (int direction in directions)
        {
            roomIndex *= direction + 2;
        }

        return roomIndex * 10;

    }


    // Метод для размещения специальных комнат
    private void PlaceSpecialRooms(int[,] grid, System.Random random, int gridSize)
    {
        // Поиск подходящей комнаты для босса
        Point bossRoom = FindAccessibleFurthestRoom(grid, startRoom);
        grid[bossRoom.X, bossRoom.Y] += 1; // 1 обозначает комнату с боссом
        roomCount++;

        // Размещение магазина и сокровищницы
        PlaceRandomRoom(grid, random, gridSize, 2); // Магазин
        PlaceRandomRoom(grid, random, gridSize, 3); // Сокровищница
    }

    // Метод для нахождения самой дальней доступной комнаты от стартовой
    private Point FindAccessibleFurthestRoom(int[,] grid, Point start)
    {
        Queue<Point> queue = new Queue<Point>();
        queue.Enqueue(start);
        Point furthestPoint = start;

        bool[,] visited = new bool[grid.GetLength(0), grid.GetLength(1)];
        visited[start.X, start.Y] = true;

        while (queue.Count > 0)
        {
            Point current = queue.Dequeue();
            furthestPoint = current;

            // Обход соседей
            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            for (int i = 0; i < 4; i++)
            {
                int nx = current.X + dx[i];
                int ny = current.Y + dy[i];

                if (nx >= 0 && nx < grid.GetLength(0) && ny >= 0 && ny < grid.GetLength(1) &&
                    (grid[nx, ny] % 10) == 0 && !visited[nx, ny])
                {
                    visited[nx, ny] = true;
                    queue.Enqueue(new Point(nx, ny));
                }
            }
        }

        return furthestPoint; // Возвращает самую дальнюю доступную комнату
    }

    // Метод для размещения случайной специальной комнаты
    private void PlaceRandomRoom(int[,] grid, System.Random random, int gridSize, int roomType)
    {
        Point room;
        do
        {
            room = new Point(random.Next(gridSize), random.Next(gridSize));
        } while ((grid[room.X, room.Y] % 10) != 0 || (room.X == startRoom.X && room.Y == startRoom.Y));
        grid[room.X, room.Y] += roomType;
        roomCount++;
    }

    // Метод для случайного перемешивания массива
    private void ShuffleArray(int[] array, System.Random random)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }






}




