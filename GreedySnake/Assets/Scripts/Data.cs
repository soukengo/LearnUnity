using System;

public abstract class Data
{
    [Serializable]
    public class Result
    {
        public int score;

        public int length;

        public Result()
        {
        }

        public Result(int score, int length)
        {
            this.score = score;
            this.length = length;
        }
    }
}