using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class LevelGrid 
{
    public int[,] levelGrid; // ������ ��� �������� ����� ������ ������
    public Point startRoom;   // ���������� ��������� �������
    public int roomCount;     // ������� ���������� ������ �� ������

    public LevelGrid(int levelnumber, int roomnumber, int seed )
    {

        LevelGenerate(levelnumber, roomnumber, seed);

    }

    // ����� ���������� ��� �������� �����
    private void LevelGenerate(int levelNumber, int approximateRoomCount, int seed)
    {
      

        // ��������� ������
        levelGrid = GenerateLevel(levelNumber, approximateRoomCount, seed);

        // ����������� ����� �� �����
       

        // ����� ��������� ��������� ������� � �������
        Console.WriteLine($"Start Room Coordinates: ({startRoom.X}, {startRoom.Y})");
    }

    // ����� ��� ��������� ������ � ��������� �����������
    private int[,] GenerateLevel(int levelNumber, int approximateRoomCount, int seed)
    {
        int gridSize = (int)Math.Ceiling(Math.Sqrt(approximateRoomCount)) * 3; // ������ ����� � �������
        levelGrid = new int[gridSize, gridSize]; // ������������� ������ �����

        // ���������� ���� ����� ��� ������ (-1)
        for (int x = 0; x < gridSize; x++)
            for (int y = 0; y < gridSize; y++)
                levelGrid[x, y] = -1;

        // ��������� ��������� ������� � ������ �����
        startRoom = new Point(gridSize / 2, gridSize / 2);
        levelGrid[startRoom.X, startRoom.Y] = 0; // 0 ���������� ������� �������
        roomCount = 1;

        // ������� ��������� ��������� ����� �� ������ ������ ������ � ����� ���������
        System.Random random = new System.Random(levelNumber * seed);

        // ��������� ������, ��������� �� ���������
        GenerateConnectedRooms(levelGrid, startRoom, gridSize, random, approximateRoomCount);

        // ���������� ����������� ������
        PlaceSpecialRooms(levelGrid, random, gridSize);

        return levelGrid;
    }

    // ����� ��� ��������� ������
    private void GenerateConnectedRooms(int[,] grid, Point start, int gridSize, System.Random random, int maxRooms)
    {
        Queue<Point> roomQueue = new Queue<Point>();
        roomQueue.Enqueue(start);
        ///////
        Queue<Point> hallQueue = new Queue<Point>(); // ������� ��� ������������ �� ������������
                                                     ///////
        grid[start.X, start.Y] = 0; // ��������� �������
        int roomCount = 1;

        int[] dx = { -1, 1, 0, 0 }; // �������� �� x ��� �������
        int[] dy = { 0, 0, -1, 1 }; // �������� �� y ��� �������

        List<int> usedDirections;

        // ���� �� ���������� ������������ ���������� ������
        while (roomQueue.Count > 0 && roomCount < maxRooms)
        {
            Point currentRoom = roomQueue.Dequeue();

            usedDirections = new List<int>();

            //���������� ���������� �������
            usedDirections.Add((grid[currentRoom.X, currentRoom.Y] / 10) - 2);

            // ������������ ����������� ��� �����������
            int[] directions = { 0, 1, 2, 3 };
            ShuffleArray(directions, random);

            // �������� ������� ������� � ������ �����������
            for (int i = 0; i < 4 && roomCount < maxRooms; i++)
            {
                int nx = currentRoom.X + dx[directions[i]];
                int ny = currentRoom.Y + dy[directions[i]];

                // �������� �� �������
                if (nx >= 0 && nx < gridSize && ny >= 0 && ny < gridSize && grid[nx, ny] == -1)
                {
                    // ����������� ������� ������ ��� ����������� ������ � ���� ��� �������
                    double distanceFactor = Math.Sqrt(Math.Pow(nx - start.X, 2) + Math.Pow(ny - start.Y, 2)) / (gridSize / 2.0);
                    double emptyChance = 0.2 + distanceFactor * 0.4; // ������� ������ �� �����

                    if (random.NextDouble() < emptyChance)
                    {
                        grid[nx, ny] = -1; // �������
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
            //������ ����������� �������
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
                    //���������� �������� ����� � ������ �������
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


    // ����� ��� ���������� ����������� ������
    private void PlaceSpecialRooms(int[,] grid, System.Random random, int gridSize)
    {
        // ����� ���������� ������� ��� �����
        Point bossRoom = FindAccessibleFurthestRoom(grid, startRoom);
        grid[bossRoom.X, bossRoom.Y] += 1; // 1 ���������� ������� � ������
        roomCount++;

        // ���������� �������� � ������������
        PlaceRandomRoom(grid, random, gridSize, 2); // �������
        PlaceRandomRoom(grid, random, gridSize, 3); // ������������
    }

    // ����� ��� ���������� ����� ������� ��������� ������� �� ���������
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

            // ����� �������
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

        return furthestPoint; // ���������� ����� ������� ��������� �������
    }

    // ����� ��� ���������� ��������� ����������� �������
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

    // ����� ��� ���������� ������������� �������
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




