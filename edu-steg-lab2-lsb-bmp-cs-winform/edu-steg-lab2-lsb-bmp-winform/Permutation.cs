using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edu_steg_lab2_lsb_bmp_winform
{
    public struct CoOrds
    {
        public int i;
        public int j;

        public CoOrds(int p1, int p2)
        {
            i = p1;
            j = p2;
        }
    }

    class Permutation
    {
        private Random generator;
        private Boolean[,] repeats;
        private int width, height;
        private int randI, randJ;
        private int[] output = new int[2];
        private int key;


        public Permutation(int key, int width, int height)
        {
            this.generator = new Random(key);
            this.repeats = new Boolean[height, width];
            this.key = key;
            this.width = width;
            this.height = height;
        }

        public CoOrds NextPair()
        {
            this.randI = this.generator.Next(0, this.height - 1);
            this.randJ = this.generator.Next(0, this.width - 1);

            while (this.repeats[randI, randJ] == true)
            {
                randI = this.generator.Next(0, this.height - 1);
                randJ = this.generator.Next(0, this.width - 1);
            }

            this.repeats[randI, randJ] = true;

            return new CoOrds(randI, randJ);
        }

        public void Reset()
        {
            this.generator = new Random(this.key);
            this.repeats = new Boolean[this.height, this.width];
        }
    }
}
