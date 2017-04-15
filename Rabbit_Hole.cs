using System;
using System.Collections.Generic;
using System.Linq;

namespace Task2_1_Rabbit_Hole
{
    public class Rabbit_Hole
    {
        public static void Main()
        {
            List<string> inputList = Console.ReadLine()
                .Split(' ')
                .ToList();

            int energy = int.Parse(Console.ReadLine());

            List<string> command = new List<string>();
            List<int> changeEnergy = new List<int>();

            InitializeLists(inputList, command, changeEnergy);

            int currentIndex = 0;

            RunCommand(command, changeEnergy, currentIndex, energy);
        }

        private static void InitializeLists(List<string> inputList, 
            List<string> command, List<int> changeEnergy)
        {
            foreach (string element in inputList)
            {
                string[] initElement = element.Split('|').ToArray();

                if (initElement[0] == "RabbitHole")
                {
                    command.Add(initElement[0]);
                    changeEnergy.Add(0);
                }
                else
                {
                    command.Add(initElement[0]);
                    changeEnergy.Add(Convert.ToInt32(initElement[1]));
                }
            }
        }

        private static void RunCommand(List<string> command, List<int> changeEnergy, 
            int currentIndex, int energy)
        {
            int length = command.Count;
            
            switch (command[currentIndex])
            {
                case "RabbitHole" :
                    Console.WriteLine("You have 5 years to save Kennedy!");
                    break;
                
                case "Left" :
                    energy -= changeEnergy[currentIndex];
                    
                    currentIndex -= changeEnergy[currentIndex] % length;
                    
                    if (currentIndex < 0)
                    {
                        currentIndex += length;
                    }
                   
                    if (energy <= 0)
                    {
                        Console.WriteLine("You are tired. You can't continue the mission.");
                    }
                    else if (command[length - 1] == "RabbitHole")
                    {
                        AddBomb(command, changeEnergy, energy);
                        RunCommand(command, changeEnergy, currentIndex, energy);
                    }
                    else 
                    {
                        ReplaceLastWithBomb(command, changeEnergy, energy, length);
                        RunCommand(command, changeEnergy, currentIndex, energy);
                    }
                    break;
                
                case "Right" :
                    energy -= changeEnergy[currentIndex];

                    currentIndex += changeEnergy[currentIndex] % length;

                    if (currentIndex > length - 1)
                    {
                        currentIndex -= length;
                    }
                    
                    if (energy <= 0)
                    {
                        Console.WriteLine("You are tired. You can't continue the mission.");
                    }
                    else if (command[length - 1] == "RabbitHole")
                    {
                        AddBomb(command, changeEnergy, energy);
                        RunCommand(command, changeEnergy, currentIndex, energy);
                    }
                    else
                    {
                        ReplaceLastWithBomb(command, changeEnergy, energy, length);
                        RunCommand(command, changeEnergy, currentIndex, energy);
                    }
                    break;
                
                case "Bomb" :
                    energy -= changeEnergy[currentIndex];
                    
                    if (energy <= 0)
                    {
                        Console.WriteLine("You are dead due to bomb explosion!");
                    }
                    else
                    {
                        command.RemoveAt(currentIndex);
                        changeEnergy.RemoveAt(currentIndex);
                        currentIndex = 0;
                        RunCommand(command, changeEnergy, currentIndex, energy);
                    }
                    break;
                
                default:
                    break;
            }
        }

        private static void ReplaceLastWithBomb(List<string> command, List<int> changeEnergy, int energy, int length)
        {
            command.RemoveAt(length - 1);
            changeEnergy.RemoveAt(length - 1);
            command.Insert(length - 1, "Bomb");
            changeEnergy.Insert(length - 1, energy);
        }

        private static void AddBomb(List<string> command, List<int> changeEnergy, int energy)
        {
            command.Add("Bomb");
            changeEnergy.Add(energy);
        }
    }
}
