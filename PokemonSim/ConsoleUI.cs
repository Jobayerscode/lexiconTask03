// UI: ConsoleUI.cs
public sealed class ConsoleUI
{
    private readonly BattleSystem _battle = new();

    public void Run()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("╔═══════════╗");
        Console.WriteLine("║POKÉMON SIM║");
        Console.WriteLine("╚═══════════╝");
        Console.ResetColor();
        Console.WriteLine();

        Console.WriteLine("Choose mode:");
        Console.WriteLine("1. Evolution Training");
        Console.WriteLine("2. Battle Mode");
        Console.Write("\nYour choice: ");
        var choice = Console.ReadLine();

        if (choice == "1") RunEvolutionDemo();
        else if (choice == "2") RunBattleMode();
        else Console.WriteLine("Invalid choice!");
    }

    // NEW: Menu to choose a Pokémon to train
    private PokemonInstance ChoosePokemonToTrain()
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== CHOOSE A POKÉMON TO TRAIN ===");
            Console.ResetColor();
            Console.WriteLine();

            var options = new List<PokemonSpecies>
            {
                SpeciesRepository.Charmander(),
                SpeciesRepository.Squirtle(),
                SpeciesRepository.Bulbasaur()
            };

            for (int i = 0; i < options.Count; i++)
            {
                var sp = options[i];
                Console.WriteLine($"{i + 1}. {sp.Name} ({sp.Type}) - Base HP {sp.BaseMaxHP}");
            }
            Console.WriteLine("0. Back");
            Console.Write("\nYour choice: ");

            if (!int.TryParse(Console.ReadLine(), out int pick))
            {
                Console.WriteLine("Invalid input. Press any key...");
                Console.ReadKey();
                continue;
            }

            if (pick == 0) return null;
            if (pick >= 1 && pick <= options.Count)
            {
                return new PokemonInstance(options[pick - 1], level: 1);
            }

            Console.WriteLine("Out of range. Press any key...");
            Console.ReadKey();
        }
    }

    private void RunEvolutionDemo()
    {
        while (true)
        {
            // Pick a single Pokémon to train
            var chosen = ChoosePokemonToTrain();
            if (chosen == null) return; // back to main menu

            Console.Clear();
            Console.WriteLine("=== EVOLUTION TRAINING ===\n");

            ShowInfo(chosen);
            Console.WriteLine($"  Says: {chosen.Speak()}\n");

            // Train to first evolution threshold if present, else default to 16
            int firstThreshold = chosen.Species.Evolution?.EvolveAtLevel ?? 16;
            while (chosen.Level < firstThreshold)
            {
                chosen.LevelUp();
                Console.WriteLine($"{chosen.Nickname} leveled up to {chosen.Level}!");
                Thread.Sleep(150);
            }

            Console.WriteLine($"\nAfter evolution (if applicable): {chosen.Speak()}");
            ShowInfo(chosen);

            // If there is a next evolution from the current species, continue
            var nextRule = chosen.Species.Evolution;
            if (nextRule != null)
            {
                Console.WriteLine("\nContinuing training to next evolution...\n");
                while (chosen.Level < nextRule.EvolveAtLevel)
                {
                    chosen.LevelUp();
                    Console.WriteLine($"{chosen.Nickname} leveled up to {chosen.Level}!");
                    Thread.Sleep(150);
                }
                Console.WriteLine($"\nFinal form (if applicable): {chosen.Speak()}");
                ShowInfo(chosen);
            }

            Console.WriteLine("\nPress any key to choose another Pokémon, or ESC to return...");
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape) break;
        }
    }

    private void RunBattleMode()
    {
        var pool = new List<PokemonInstance>
        {
            new(SpeciesRepository.Charmander(), 5),
            new(SpeciesRepository.Squirtle(), 5),
            new(SpeciesRepository.Bulbasaur(), 5),
            new(SpeciesRepository.Charmeleon(), 20),
            new(SpeciesRepository.Wartortle(), 20),
            new(SpeciesRepository.Ivysaur(), 20)
        };

        PokemonInstance current = null;
        var rng = new Random();
        bool play = true;

        while (play)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════╗");
            Console.WriteLine("║POKÉMON BATTLE SIM║");
            Console.WriteLine("╚══════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            if (current != null)
            {
                Console.WriteLine($"Current Pokémon: {current.Nickname} (Level {current.Level})");
                Console.WriteLine("1. Continue with current Pokémon");
                Console.WriteLine("2. Choose new Pokémon");
                Console.Write("\nYour choice: ");
                var c = Console.ReadLine();
                if (c != "1") current = null;
            }

            if (current == null)
            {
                Console.WriteLine("Choose your Pokémon:");
                for (int i = 0; i < pool.Count; i++)
                    Console.WriteLine($"{i + 1}. {pool[i].Species.Name} - {pool[i].Species.Type} (Lv {pool[i].Level})");

                Console.Write("\nYour choice: ");
                if (!int.TryParse(Console.ReadLine(), out int pick) || pick < 1 || pick > pool.Count)
                {
                    Console.WriteLine("Invalid choice!");
                    continue;
                }
                // clone simple
                var sel = pool[pick - 1];
                current = new PokemonInstance(sel.Species, sel.Level, sel.Attacks);
            }

            var opponentCandidates = pool.Where(p => p.Species.Name != current.Species.Name).ToList();
            var opponentBase = opponentCandidates[rng.Next(opponentCandidates.Count)];
            var opponent = new PokemonInstance(opponentBase.Species, opponentBase.Level, opponentBase.Attacks);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Your opponent chose {opponent.Species.Name}!");
            Console.ResetColor();
            Thread.Sleep(1000);

            RunBattleLoop(current, opponent);

            Console.WriteLine("\n\nPlay again? (y/n): ");
            play = Console.ReadLine()?.Trim().ToLowerInvariant() == "y";
        }

        Console.WriteLine("\nThanks for playing!");
    }

    private void RunBattleLoop(PokemonInstance player, PokemonInstance foe)
    {
        while (!player.IsFainted && !foe.IsFainted)
        {
            DisplayBattleField(player, foe);

            Console.WriteLine("\n━━━━━━━━━━━━━━━━ YOUR TURN ━━━━━━━━━━━━━━━━");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. View Info");
            Console.WriteLine("3. Hear Pokémon");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                for (int i = 0; i < player.Attacks.Count; i++)
                {
                    var a = player.Attacks[i];
                    Console.WriteLine($"{i + 1}. {a.Name} (Base {a.BasePower}, {a.Type})");
                }
                if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 1 && idx <= player.Attacks.Count)
                {
                    var a = player.Attacks[idx - 1];
                    var res = _battle.UseAttack(player, a, foe);
                    PrintAttackResult(res);
                }
                else Console.WriteLine("Invalid choice! Turn skipped.");
            }
            else if (choice == "2")
            {
                ShowInfo(player);
                ShowInfo(foe);
                continue;
            }
            else if (choice == "3")
            {
                Console.WriteLine(player.Speak());
                continue;
            }
            else
            {
                Console.WriteLine("Invalid choice!");
                continue;
            }

            if (foe.IsFainted) { PrintVictory(player, foe); player.LevelUp(); Console.WriteLine(player.Speak()); break; }

            Thread.Sleep(800);
            Console.WriteLine("\n━━━━━━━━━━━━━━ OPPONENT'S TURN ━━━━━━━━━━━━━━━");
            var oa = _battle.ChooseRandomAttack(foe);
            var ores = _battle.UseAttack(foe, oa, player);
            PrintAttackResult(ores);

            if (player.IsFainted) { PrintDefeat(player, foe); break; }
            Thread.Sleep(800);
        }
    }

    private void DisplayBattleField(PokemonInstance player, PokemonInstance foe)
    {
        Console.WriteLine("\n╔═══════════════════════ BATTLEFIELD ═══════════════════════╗\n");
        Console.Write("  OPPONENT: "); ShowInfo(foe);
        Console.WriteLine();
        Console.Write("  YOU: "); ShowInfo(player);
        Console.WriteLine("\n╚════════════════════════════════════════════════════════════╝");
    }

    private void ShowInfo(PokemonInstance p)
    {
        Console.ForegroundColor = GetTypeColor(p.Species.Type);
        Console.WriteLine($"{p.Nickname} - Level {p.Level}");
        Console.WriteLine($"  Type: {p.Species.Type}");
        Console.WriteLine($"  HP: {p.CurrentHP}/{p.MaxHP}");
        DisplayHealthBar(p);
        Console.ResetColor();
    }

    private void DisplayHealthBar(PokemonInstance p)
    {
        int barLength = 20;
        int filled = (int)((double)p.CurrentHP / p.MaxHP * barLength);
        Console.Write("  HP: [");
        Console.ForegroundColor = p.CurrentHP > p.MaxHP * 0.5 ? ConsoleColor.Green :
                                  p.CurrentHP > p.MaxHP * 0.25 ? ConsoleColor.Yellow : ConsoleColor.Red;
        Console.Write(new string('█', Math.Max(0, filled)));
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(new string('░', Math.Max(0, barLength - filled)));
        Console.ResetColor();
        Console.WriteLine("]");
    }

    private void PrintAttackResult(BattleSystem.AttackResult r)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"{r.Attacker} used {r.AttackName}! ");
        Console.ResetColor();

        if (r.Multiplier > 1.0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"It's super effective! Dealt {r.Damage} damage!");
        }
        else if (r.Multiplier < 1.0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"It's not very effective... Dealt {r.Damage} damage.");
        }
        else
        {
            Console.WriteLine($"Dealt {r.Damage} damage!");
        }
        Console.ResetColor();
    }

    private void PrintVictory(PokemonInstance player, PokemonInstance foe)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n VICTORY! ");
        Console.WriteLine($"{player.Nickname} defeated {foe.Nickname}!");
        Console.ResetColor();
        Console.WriteLine("\nPress any key to level up...");
        Console.ReadKey();
    }

    private void PrintDefeat(PokemonInstance player, PokemonInstance foe)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n  DEFEAT! ");
        Console.WriteLine($"{player.Nickname} was defeated by {foe.Nickname}!");
        Console.ResetColor();
    }

    private static ConsoleColor GetTypeColor(ElementType t) => t switch
    {
        ElementType.Fire => ConsoleColor.Red,
        ElementType.Water => ConsoleColor.Cyan,
        ElementType.Grass => ConsoleColor.Green,
        _ => ConsoleColor.White
    };
}
