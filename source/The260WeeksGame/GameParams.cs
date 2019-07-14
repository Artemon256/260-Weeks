namespace The260WeeksGame
{
    public class GameParams
    {
        public int NumberOfBusinessmen;
        public int NumberOfMassMedia;
        public DifficultyLevel Difficulty;

        public enum DifficultyLevel {
            Easy,
            Moderate,
            Medium,
            Hard,
            Nightmare
        }


        private GameParams() {

        }

        public static GameParams instance;

        public static GameParams getInstance() {
            if (instance == null)
                instance = new GameParams();
            return instance;
        }
    }
}
