using LuccaDevises;

namespace LuccaDevisesTest
{
    [TestClass]
    public class HelperTest
    {
        static readonly string pathFile = @"../../../TestTauxDeChange.txt";
        static readonly string pathErrorFile = @"../../../TestTauxDeChangeError.txt";
        [TestMethod]
        public void ReadCorrectFirstLine()
        {
            Tuple<string, int, string> firstLine = Helper.ReadFirstLine(File.ReadAllLines(pathFile).ToList().First());

            Assert.IsNotNull(firstLine);
        }

        [TestMethod]
        public void ReadErrorFirstLine()
        {
            Tuple<string, int, string> firstLine = Helper.ReadFirstLine(File.ReadAllLines(pathErrorFile).ToList().First());

            Assert.IsNull(firstLine);
        }

        [TestMethod]
        public void ReadEmptyFirstLine()
        {
            Tuple<string, int, string> firstLine = Helper.ReadFirstLine(string.Empty);

            Assert.IsNull(firstLine);
        }

        [TestMethod]
        public void ReadCorrectSecondLine()
        {
            int numberOfRate = Helper.ReadSecondLine(File.ReadAllLines(pathFile).ToList().Skip(1).First());

            Assert.IsNotNull(numberOfRate);
        }

        [TestMethod]
        public void ReadErrorSecondLine()
        {
            int numberOfRate = Helper.ReadSecondLine(File.ReadAllLines(pathErrorFile).ToList().Skip(1).First());

            Assert.AreEqual(0, numberOfRate);
        }

        [TestMethod]
        public void ReadEmptySecondLine()
        {
            int numberOfRate = Helper.ReadSecondLine(string.Empty);

            Assert.AreEqual(0, numberOfRate);
        }

        [TestMethod]
        public void ReadCorrectOtherLine()
        {
            // Lecture du fichier
            List<string> lines = File.ReadAllLines(pathFile).ToList();
            List<string> currencyTab = new List<string>(lines);
            currencyTab.RemoveRange(0, 2);
            List<Tuple<string, string, decimal>> rateLines = Helper.ReadOtherLines(currencyTab);

            Assert.IsNotNull(rateLines);
        }

        [TestMethod]
        public void ReadErrorOtherLine()
        {
            // Lecture du fichier
            List<string> lines = File.ReadAllLines(pathErrorFile).ToList();
            List<string> currencyTab = new List<string>(lines);
            currencyTab.RemoveRange(0, 2);
            List<Tuple<string, string, decimal>> rateLines = Helper.ReadOtherLines(currencyTab);

            Assert.IsNull(rateLines);
        }
    }
}