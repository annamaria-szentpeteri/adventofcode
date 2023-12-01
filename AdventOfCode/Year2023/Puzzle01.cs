namespace Year2023
{
    internal class Puzzle01 : IPuzzle<int>
    {
        private readonly string _inputFileName = string.Empty;

        public Puzzle01(string inputFileName)
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

                char firstNumericChar = '0';
                bool foundFirst = false;

                char lastNumericChar = '0';
                bool foundLast = false;

                for (int i = 0; i < line.Length; i++)
                {
                    if (!foundFirst && char.IsDigit(line[i]))
                    {
                        firstNumericChar = line[i];
                        foundFirst = true;
                    }

                    if (!foundLast && char.IsDigit(line[line.Length - i - 1]))
                    {
                        lastNumericChar = line[line.Length - i - 1];
                        foundLast = true;
                    }

                    if (foundFirst && foundLast)
                    {
                        result += int.Parse(string.Concat(firstNumericChar, lastNumericChar));
                        break;
                    }
                }
            }

            return result;
        }
    }
}
