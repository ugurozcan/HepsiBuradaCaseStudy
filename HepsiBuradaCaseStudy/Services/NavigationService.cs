using HepsiBuradaCaseStudy.Model;
using System.Collections.Generic;
using System.Linq;

namespace HepsiBuradaCaseStudy.Services
{
    public class NavigationService
    {

        protected readonly IReadOnlyCollection<(char CurrentDirection, char Instruction, char NewDirection)> DirectionFinding
            = new List<(char CurrentDirection, char Instruction, char NewDirection)>
            {
                ('N', 'R', 'E'),
                ('E', 'R', 'S'),
                ('S', 'R', 'W'),
                ('W', 'R', 'N'),
                ('N', 'L', 'W'),
                ('W', 'L', 'S'),
                ('S', 'L', 'E'),
                ('E', 'L', 'N')
            };

        public virtual string Navigate(IList<string> instructions)
        {
            var parsedInstructionsTuple = ValidateAndParseInsutrctions(instructions);

            if (!parsedInstructionsTuple.isValid) return "Please check the parameters!";

            var xLength = parsedInstructionsTuple.instructions.Plateau.XTotalLength;
            var yLength = parsedInstructionsTuple.instructions.Plateau.YTotalLength;
            var xStartingPosition = parsedInstructionsTuple.instructions.StartingPosition.XStartingPosition;
            var yStartingPosition = parsedInstructionsTuple.instructions.StartingPosition.YStartingPosition;
            var direction = parsedInstructionsTuple.instructions.StartingPosition.DirectionFacing;

            var position = SetStartingPosition(xStartingPosition, yStartingPosition, direction);

            foreach (var instruction in parsedInstructionsTuple.instructions.MovementInstructions)
            {
                position = StartMoving(instruction, xLength, yLength, position);
            }

            return $"{position.X} {position.Y} {position.Direction}";
        }

        private (bool isValid, Instructions instructions) ValidateAndParseInsutrctions(IList<string> instructions)
        {
            if (!instructions.Any() || instructions.Count != 3)
                return (isValid: false, instructions: null);

            var plateau = instructions[0].Split(' ');
            if (plateau.Length != 2)
                return (isValid: false, instructions: null);

            var startingPosition = instructions[1].Split(' ');
            if (startingPosition.Length != 3)
                return (isValid: false, instructions: null);

            var movementInstructions = instructions[2].ToArray();

            var invalidInputsList = new List<bool>
            {
                int.TryParse(plateau[0], out var xTotalLength),
                int.TryParse(plateau[1], out var yTotalLength),
                int.TryParse(startingPosition[0], out var xStartingPosition),
                int.TryParse(startingPosition[1], out var yStartingPosition)
            };

            if (plateau.Length != 2 || startingPosition.Length != 3 || invalidInputsList.Contains(false))
                return (isValid: false, null);

            return (isValid: true, instructions: new Instructions
            {
                Plateau = (xTotalLength, yTotalLength),
                StartingPosition = (xStartingPosition, yStartingPosition, startingPosition[2].FirstOrDefault().ToString()),
                MovementInstructions = movementInstructions
            });
        }

        protected virtual Position StartMoving(char instruction, int xLength, int yLength, Position currentPosition)
        {
            if (instruction != 'M')
                return SetNewDirection(instruction, currentPosition);

            return Go(currentPosition);

        }

        protected virtual Position SetNewDirection(char instruction, Position currentPosition)
        {
            var lookupValue = DirectionFinding
                 .FirstOrDefault(lookup => lookup.CurrentDirection == currentPosition.Direction.ToCharArray().FirstOrDefault() && lookup.Instruction == instruction);

            currentPosition.Direction = lookupValue.NewDirection.ToString();

            return currentPosition;
        }

        protected virtual Position Go(Position currentPosition)
        {
            var movementAxis = currentPosition.Direction == "E" || currentPosition.Direction == "W" ? "X" : "Y";
            var xPosition = currentPosition.X;
            var yPosition = currentPosition.Y;

            if (movementAxis == "X")
            {
                xPosition = currentPosition.Direction == "W" ? currentPosition.X - 1 : currentPosition.X + 1;
            }
            else
            {
                yPosition = currentPosition.Direction == "S" ? currentPosition.Y - 1 : currentPosition.Y + 1;
            }
            return new Position { Direction = currentPosition.Direction, X = xPosition, Y = yPosition };
        }

        protected Position SetStartingPosition(int x, int y, string direction)
        {
            return new Position { X = x, Y = y, Direction = direction };
        }

    }
}
