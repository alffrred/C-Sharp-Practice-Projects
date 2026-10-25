using System;

class Program
{
    static void Main(string[] args)
    {
        int day = 1;
        int location = 0; // 0: Space Station 1: Mercury 2: Mars 3: Europa
        string locationStr = "Space Station";

        int credits = 100, EarnedCredits = 0;
        int fuel = 60, fuelCost = 0;
        int oxygen = 70, oxygenCost = 0;
        int energy = 65, energyCost = 0;
        int Health = 100, requiredHealth = 0, HealthCost = 0;
        int score = 0, EarnedScore = 0;
        int minerals = 0, mercuryMinerals = 0;
        int operations = 0;

        bool gameOver = false, lifelineUsed = false, endday = false;
        bool mercuryExplored = false, marsExplored = false, europaExplored = false, notExplored = false, lastActionWasExploration = false;
        bool mercuryCollectedToday = false, marsCollectedToday = false, europaCollectedToday = false;

        while (!gameOver)
        {
            endday = false; //When the day ends variable should be set back to false

            //Printing menu
            Console.WriteLine("========== SPACE MISSION SIMULATOR ==========");
            Console.WriteLine($"Day: {day}");
            Console.WriteLine($"Current Location: {locationStr}");
            Console.WriteLine($"Credits: {credits} C");
            Console.WriteLine($"Fuel: {fuel} / 100");
            Console.WriteLine($"Oxygen: {oxygen} / 100");
            Console.WriteLine($"Energy: {energy} / 100");
            Console.WriteLine($"Health: {Health} / 100");
            Console.WriteLine($"Mission Score: {score}");
            Console.WriteLine($"Inventory: {minerals} Minerals");
            Console.WriteLine("1. Travel to a Planet");
            Console.WriteLine("2. Explore the Planet");
            Console.WriteLine("3. Collect Minerals");
            Console.WriteLine("4. Supply Station");
            Console.WriteLine("5. Display Mission Status");
            Console.WriteLine("6. End of the Day");
            Console.WriteLine("7. Quit Mission");

            //Getting input from user for menu action
            Console.Write("Enter your choice: ");
            string ChoiceStr = Console.ReadLine();
            int choice;
            if (ChoiceStr == "1") choice = 1;
            else if (ChoiceStr == "2") choice = 2;
            else if (ChoiceStr == "3") choice = 3;
            else if (ChoiceStr == "4") choice = 4;
            else if (ChoiceStr == "5") choice = 5;
            else if (ChoiceStr == "6") choice = 6;
            else if (ChoiceStr == "7") choice = 7;
            else choice = 0;

            if (choice == 1) //Travel
            {
                Console.Write("Select destination (1: Mercury, 2: Mars, 3: Europa): ");
                string destStr = Console.ReadLine();
                int destination;
                if (destStr == "1") destination = 1;
                else if (destStr == "2") destination = 2;
                else if (destStr == "3") destination = 3;
                else destination = -1;

                bool europaUnlocked = mercuryMinerals >= 4 && marsExplored && mercuryExplored;

                if (destination == location) Console.WriteLine("You are already on this planet!");
                else if (destination == -1) Console.WriteLine("Invalid destination.");
                else if (destination == 3 && !europaUnlocked)
                {
                    Console.WriteLine("Europa is currently locked!");
                    Console.WriteLine("Missing requirements:");
                    Console.WriteLine($"- Collect at least 4 minerals from Mercury. (Current: {mercuryMinerals})");
                    Console.WriteLine("- Complete Mars exploration.");
                    Console.WriteLine("Travel is cancelled.");
                }
                else
                {
                    switch (destination)
                    {
                        case 1:
                            fuelCost = 10;
                            oxygenCost = 8;
                            destStr = "Mercury";
                            break;
                        case 2:
                            fuelCost = 15;
                            oxygenCost = 12;
                            destStr = "Mars";
                            break;
                        case 3:
                            fuelCost = 20;
                            oxygenCost = 16;
                            destStr = "Europa";
                            break;
                    }
                    if (fuel >= fuelCost && oxygen >= oxygenCost)
                    {
                        fuel -= fuelCost;
                        oxygen -= oxygenCost;
                        location = destination;
                        locationStr = destStr;
                        Console.WriteLine($"Travel successful! Welcome to {locationStr}.");
                        Console.WriteLine($"Fuel consumed: {fuelCost}. Remaining: {fuel}");
                        Console.WriteLine($"Oxygen consumed: {oxygenCost}. Remaining: {oxygen}");
                    }
                    else Console.WriteLine("Insufficient fuel or oxygen for travel.");
                }
            }
            else if (choice == 2) //Explore
            {
                if (location == 0) Console.WriteLine("You cannot explore the Space Station."); //Checks invalidity
                else if (location == 1 && !mercuryExplored) //Checks Explorability
                {
                    notExplored = true;
                    requiredHealth = 31;
                    energyCost = 12;
                    HealthCost = 5;
                    EarnedCredits = 15;
                    EarnedScore = 10;
                }
                else if (location == 2 && !marsExplored)
                {
                    notExplored = true;
                    requiredHealth = 50;
                    energyCost = 18;
                    HealthCost = 10;
                    EarnedCredits = 25;
                    EarnedScore = 20;
                }
                else if (location == 3 && !europaExplored)
                {
                    notExplored = true;
                    requiredHealth = 60;
                    energyCost = 25;
                    HealthCost = 15;
                    EarnedCredits = 40;
                    EarnedScore = 30;
                }
                else Console.WriteLine($"{locationStr} is already explored.");

                if (notExplored) //If the planet is explorable
                {
                    notExplored = false;
                    if (Health >= requiredHealth)
                    {
                        if (energy >= energyCost)
                        {
                            energy -= energyCost;
                            Health -= HealthCost;
                            credits += EarnedCredits;
                            score += EarnedScore;
                            operations++;
                            Console.WriteLine("Exploration successful!");
                            Console.WriteLine($"Energy consumed: {energyCost}. Remaining: {energy}");
                            Console.WriteLine($"Spaceship health lost: {HealthCost}. Remaining: {Health}");
                            Console.WriteLine($"Earned {EarnedCredits} Credits and {EarnedScore} Points.");
                            if (lastActionWasExploration)
                            {
                                Console.WriteLine("Consecutive Exploration Bonus! +10 Mission Points");
                                score += 10;
                            }
                            else lastActionWasExploration = true;
                            Console.WriteLine($"Current successful field operations: {operations}");
                            switch (location)
                            {
                                case 1: mercuryExplored = true; break;
                                case 2: marsExplored = true; break;
                                case 3: europaExplored = true; break;
                            }
                        }
                        else Console.WriteLine("Insufficient energy to explore.");
                    }
                    else Console.WriteLine($"Warning: Spaceship health must be above {requiredHealth}. Repair needed!");
                }
            }
            else if (choice == 3) //Collect Minerals
            {
                //Checks for Invalidities
                if (location == 0) Console.WriteLine("You cannot collect minerals at the Space Station.");
                else if ((location == 1 && !mercuryExplored) || (location == 2 && !marsExplored) || (location == 3 && !europaExplored))
                    Console.WriteLine($"You must explore {locationStr} before collecting minerals.");
                else if ((location == 1 && mercuryCollectedToday) || (location == 2 && marsCollectedToday) || (location == 3 && europaCollectedToday))
                    Console.WriteLine($"Daily collection limit reached for {locationStr}.");
                else
                {
                    energyCost = 6 + 2 * location;
                    if (energy >= energyCost) //Checks energy sufficency
                    {
                        energy -= energyCost;
                        minerals += 1 + location;
                        operations++;
                        lastActionWasExploration = false;
                        if (location == 1) mercuryMinerals += 1 + location;
                        Console.WriteLine("Mineral collection successful!");
                        Console.WriteLine($"Energy consumed: {6 + 2 * location}. Remaining: {energy}");
                        Console.WriteLine($"Collected {1 + location} minerals from {locationStr}.");
                        Console.WriteLine($"Current successful field operations: {operations}");

                        switch (location)
                        {
                            case 1: mercuryCollectedToday = true; break;
                            case 2: marsCollectedToday = true; break;
                            case 3: europaCollectedToday = true; break;
                        }
                    }
                    else Console.WriteLine("Insufficient energy.");
                }
            }
            else if (choice == 4) //Supply Station
            {
                bool inSupplyMenu = true;
                //Printing Supply Station Menu
                Console.WriteLine("--- SUPPLY STATION ---");
                Console.WriteLine("1. Buy Fuel (5 fuel for 12 C)");
                Console.WriteLine("2. Buy Oxygen (5 oxygen for 10 C)");
                Console.WriteLine("3. Recharge Energy (20 energy for 10 C)");
                Console.WriteLine("4. Repair Spaceship (20 health for 15 C)");
                Console.WriteLine("5. Sell All Minerals (6 C per mineral)");
                Console.WriteLine("6. Return to Main Menu");

                while (inSupplyMenu)
                {
                    Console.WriteLine();
                    Console.Write("Enter your choice: ");
                    string sChoiceStr = Console.ReadLine();

                    if (sChoiceStr == "1") //Buy Fuel
                    {
                        if (credits >= 12) //Checks credit sufficency
                        {
                            fuel += 5;
                            credits -= 12;
                            if (fuel >= 100) fuel = 100;
                            Console.WriteLine("Purchased 5 Fuel.");
                            Console.WriteLine($"Current Credits: {credits}C. Current Fuel: {fuel}.");
                        }
                        else Console.WriteLine("Insufficient credits.");
                    }
                    else if (sChoiceStr == "2") //Buy Oxygen
                    {
                        if (credits >= 10)
                        {
                            oxygen += 5;
                            credits -= 10;
                            if (oxygen >= 100) oxygen = 100;
                            Console.WriteLine("Purchased 5 Oxygen.");
                            Console.WriteLine($"Current Credits: {credits}C. Current Oxygen: {oxygen}.");
                        }
                        else Console.WriteLine("Insufficient credits.");
                    }
                    else if (sChoiceStr == "3") //Recharge Energy
                    {
                        if (credits >= 10)
                        {
                            energy += 20;
                            credits -= 10;
                            if (energy >= 100) energy = 100;
                            Console.WriteLine("Recharged 20 Energy.");
                            Console.WriteLine($"Current Credits: {credits}C. Current Energy: {energy}.");
                        }
                        else Console.WriteLine("Insufficient credits.");
                    }
                    else if (sChoiceStr == "4") //Repair Spaceship
                    {
                        if (credits >= 15)
                        {
                            Health += 20;
                            credits -= 15;
                            if (Health >= 100) Health = 100;
                            Console.WriteLine("Repaired 20 Health.");
                            Console.WriteLine($"Current Credits: {credits}C. Current Health: {Health}.");
                        }
                        else Console.WriteLine("Insufficient credits.");
                    }
                    else if (sChoiceStr == "5") //Sell All Minerals
                    {
                        if (minerals > 0)
                        {
                            credits += 6 * minerals;
                            Console.WriteLine($"Sold {minerals} minerals for {minerals * 6} C.");
                            Console.WriteLine($"Current Credits: {credits} C.");
                            minerals = 0;
                        }
                        else Console.WriteLine("You have no minerals to sell.");
                    }
                    else if (sChoiceStr == "6") //Return to Main Menu
                    {
                        inSupplyMenu = false; //Ends the while loop
                        Console.WriteLine("Returning to main menu...");
                    }
                    else Console.WriteLine("Invalid option.");
                }
            }
            else if (choice == 5) // The menu will be printed in the next iteration.
                Console.WriteLine("Mission status displayed.");
            else if (choice == 6) // End of the Day
            {
                if (operations >= 2) endday = true;
                else Console.WriteLine("You must complete at least 2 successful field operations today to end the day manually.");
            }
            else if (choice == 7) //Quit Mission
            {
                Console.WriteLine("Quitting Mission...");
                Console.WriteLine("Mission Failed. Game Over!");
                gameOver = true;
            }
            else Console.WriteLine("Invalid choice. Please try again.");
            //Checking conditions
            if (mercuryExplored && marsExplored && europaExplored) //If all the planets are discovered, the game ends.
            {
                score += 50;
                Console.WriteLine("*** ALL PLANETS EXPLORED! FINAL BONUS: +50 POINTS ***");
                Console.WriteLine("MISSION ACCOMPLISHED!");
                Console.WriteLine($"Final Mission Score: {score}");
                Console.WriteLine("Thank you for playing Space Mission Simulator!");
                gameOver = true;
            }
            if (operations >= 5) //If the number of operations exceeds 5, the day ends automatically.
            {
                Console.WriteLine("Maximum field operations (5) reached for today.");
                endday = true;
            }
            if (endday) //Ends the day
            {
                Console.WriteLine("It is the end of the day!");
                oxygen -= 5;
                energy += 15;
                if (energy >= 100) energy = 100; // Maximum value of energy is 100.

                Console.WriteLine("5 Oxygen consumed. Remaining: " + oxygen);
                Console.WriteLine("15 Energy restored. Remaining: " + energy);
                Console.WriteLine("==================================================================================="); //Printed after end of the day

                day++;
                operations = 0;
                lastActionWasExploration = false;
                mercuryCollectedToday = false;
                marsCollectedToday = false;
                europaCollectedToday = false;
            }
            if (oxygen <= 0 || Health <= 0) //If oxygen or health drops below 0, the game ends.
            {
                Console.WriteLine("Critical Failure: Oxygen or Health depleted! GAME OVER.");
                gameOver = true;
            }
            if (oxygen <= 15 && !lifelineUsed && !gameOver) //If oxygen levels fall below 15, Lifeline activates once, providing 20 oxygen.
            {
                oxygen += 20;
                Console.WriteLine("*** EMERGENCY LIFELINE ACTIVATED: +20 Oxygen added. ***");
                lifelineUsed = true;
            }
            if (!endday && !gameOver) //Printed after every action in a day
                Console.WriteLine("-----------------------------------------------------------------------------------");
        }
    }
}