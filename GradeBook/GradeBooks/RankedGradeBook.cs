using System;
using System.Linq;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");

            char GetGrade(int index)
            {
                switch (index)
                {
                    case 0:
                        return 'A';
                    case 1:
                        return 'B';
                    case 2:
                        return 'C';
                    case 3:
                        return 'D';
                    default:
                        return 'F';
                }
            }

            var x = (double)Students.Count / 100 * 20;

            var averageGrades = Students
                .Select(s => s.AverageGrade)
                .OrderByDescending(a => a)
                .ToList();

            var studentWithHigherGrade = averageGrades.Count(a => a > averageGrade);

            if (studentWithHigherGrade < x)
                return 'A';
            if (studentWithHigherGrade < x * 2)
                return 'B';
            if (studentWithHigherGrade < x * 3)
                return 'C';
            if (studentWithHigherGrade < x * 4)
                return 'D';

            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStatistics();
        }


        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStudentStatistics(name);
        }
    }
}