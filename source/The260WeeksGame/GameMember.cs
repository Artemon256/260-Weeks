using System;
using System.Collections.Generic;

namespace The260WeeksGame
{
    public abstract class GameMember
    {

        private static int numberOfMembers = 0;
        private static Dictionary<int, GameMember> idMemberPairs = new Dictionary<int, GameMember>();

        public static readonly double MaxOpinion = 300;
        public static readonly double MinOpinion = -300;

        protected int id = 0;
        protected string name="";
        public Dictionary<GameMember, double> Opinions;

        


        public GameMember(string name) {
            numberOfMembers++;

            this.name = name;
            id = numberOfMembers;
            Opinions = new Dictionary<GameMember, double>();
            idMemberPairs[id] = this;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }
        }

        

        public static double Adjust(double value)
        {
            const double bound = 0.99999999999999991;

            double result = (Math.Atan(Math.Exp(0.06 * value)) * 4)/Math.PI - 1;

            return ConstrainValue(result, -bound, bound);
        }

        public static double Unadjust(double value)
        {
            if (value <= -1)
                return -double.NegativeInfinity;
            if (value >= 1)
                return double.PositiveInfinity;

            return Math.Log(Math.Tan((value + 1d) * Math.PI * 0.25d)) / 0.06d;
        }

        public static double ConstrainOpinion(double opinion)
        {
            return ConstrainValue(opinion, MinOpinion, MaxOpinion);
        }

        public static double ConstrainValue(double value, double leftBound, double rightBound)
        {
            if (value >= rightBound)
                return rightBound;
            if (value <= leftBound)
                return leftBound;
            return value;
        }

        public static GameMember GetGameMemberById(int id)
        {
            GameMember result = null;
            if(idMemberPairs.TryGetValue(id, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public void RevaluateOpinionsWeighted()
        {
            Dictionary<GameMember, double> OpinionsCopy = new Dictionary<GameMember, double>(Opinions);
            foreach (KeyValuePair<GameMember, double> subject in Opinions)
            {
                double value = Adjust(subject.Value);
                foreach (KeyValuePair<GameMember, double> reference in Opinions)
                {
                    if (reference.Key == subject.Key)
                        continue;
                    double referenceValue;
                    if (reference.Key.Opinions.TryGetValue(subject.Key, out referenceValue))
                        value += Adjust(reference.Value) * Adjust(referenceValue) * 0.01; // An enemy of my enemy is my friend
                }
                OpinionsCopy[subject.Key] = ConstrainOpinion(Unadjust(value));
            }
            Opinions = OpinionsCopy;
        }

        public virtual void Turn()
        {
            RevaluateOpinionsWeighted();
        }

        public abstract void GenerateOpinions();
        public abstract void RevaluateOpinion(GameMember sender, GameMember target, double delta);
    }
}