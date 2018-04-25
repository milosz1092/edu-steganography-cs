using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edu_steg_lab2_lsb_bmp_winform
{
    class Correction
    {
        public static BitArray Decode(BitArray inputBits)
        {
            BitArray outputBits = new BitArray((inputBits.Length / 5));
            byte negative = 0, positive = 0, counter = 0;
            int outputIndex = 0;
            int i = 0;

            for (i = 0; i < inputBits.Length; i++)
            {
                if (counter < 5)
                {
                    if (inputBits.Get(i))
                    {
                        positive++;
                    }
                    else
                    {
                        negative++;
                    }

                    counter++;

                    if (counter == 5)
                    {
                        if (negative > positive)
                        {
                            outputBits.Set(outputIndex, false);
                        } else
                        {
                            outputBits.Set(outputIndex, true);
                        }


                        
                        counter = 0; negative = 0; positive = 0;
                        outputIndex++;
                    }
                }
            }

            return outputBits;
        }

        public static BitArray Encode(BitArray inputBits)
        {
            BitArray outputBits = new BitArray((inputBits.Length * 5));
            Boolean bitToSet;
            int counter = 0;
            int i, j;
            
            for (i=0; i<inputBits.Length; i++)
            {
                if (inputBits.Get(i))
                {
                    bitToSet = true;
                } else
                {
                    bitToSet = false;
                }

                for (j=0; j<5; j++)
                {
                    outputBits.Set(counter, bitToSet);
                    counter++;
                }
            }

            return outputBits;
        }
    }
}
