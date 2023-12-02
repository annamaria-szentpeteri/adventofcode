using Year2023.Exceptions;

namespace Year2023.Solutions
{
    internal class PuzzleDay02_1 : IPuzzle
    {
        private record Reveal(int RedCubes, int GreenCubes, int BlueCubes, bool IsPossible);
        private record Game(int GameID, List<Reveal> Reveals);

        private const int RedCubes = 12;
        private const int GreenCubes = 13;
        private const int BlueCubes = 14;

        private readonly string _inputFileName = string.Empty;

        public PuzzleDay02_1(string inputFileName)
        {
            _inputFileName = inputFileName;
        }

        public int SolvePuzzle()
        {
            var result = 0;

            using var file = File.OpenText(_inputFileName);

            while (!file.EndOfStream)
            {
                var line = file.ReadLine();

                if (string.IsNullOrEmpty(line))
                    continue;

                var game = ParseGame(line);

                result += game.Reveals.Exists(r => r.IsPossible == false) ? 0 : game.GameID;
            }

            return result;
        }

        private static Game ParseGame(string line)
        {
            var sections = line.Split(new char[] { ':', ';' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            var gameID = GetGameID(sections[0]);

            var reveals = new List<Reveal>(sections.Length - 1);
            for (int i = 1; i < sections.Length; i++)
            {
                var reveal = CreateReveal(sections[i]);
                reveals.Add(reveal);
            }

            return new Game(gameID, reveals);
        }

        private static int GetGameID(string input)
        {
            return int.Parse(input[input.IndexOf(' ')..]);
        }

        private static Reveal CreateReveal(string input)
        {
            var red = 0;
            var green = 0;
            var blue = 0;
            var isPossible = true;

            input.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .ToList()
                .ForEach(x => 
                    {
                        var sections = x.Split(' ');
                        var colour = sections[1];
                        var cubeCount = int.Parse(sections[0]);

                        switch (colour)
                        {
                            case "red":
                                {
                                    red = cubeCount;
                                    if (red > RedCubes)
                                        isPossible = false;
                                    break;
                                }
                            case "green":
                                {
                                    green = cubeCount;
                                    if (green > GreenCubes)
                                        isPossible = false;
                                    break;
                                }
                            case "blue":
                                {
                                    blue = cubeCount;
                                    if (blue > BlueCubes)
                                        isPossible = false;
                                    break;
                                }
                            default:
                                throw new UnknownColourException(colour);
                        }
                    });

            return new Reveal(red, green, blue, isPossible);
        }
    }
}
