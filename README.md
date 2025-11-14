# lexiconTask03

🏗️ Project Structure

ElementType – Fire, Water, Grass.
Attack / LegendaryAttack – Standard and boosted move types.
PokemonSpecies – Immutable species data type, HP, moves, evolution.
EvolutionRule – Level threshold + next species factory.
PokemonInstance – Level, HP, attacks, and evolution handling.
TypeEffectiveness – Damage multipliers.
BattleSystem – Battle logic returning structured results.

📚 Data

AttackCatalog – Predefined attacks and legendary moves.
SpeciesRepository – Starter evolution lines with thresholds.

🖥️ UI.Console

ConsoleUI – Menus, rendering, input, training, and battle loops.
Program – Composition root that wires everything up.
