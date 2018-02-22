namespace stealFight {

    public class MainLogic : BaseLogic {
        private static MainLogic instance;

        public static MainLogic Instance {
            get { return instance ?? (instance = new MainLogic()); }
        }

        public override void Init() {
        }
    }

}