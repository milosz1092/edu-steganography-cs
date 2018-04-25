using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace edu_steg_lab2_lsb_bmp_winform
{
    class Steganography
    {

        public static BitArray PrependBA(BitArray current, BitArray before)
        {
            var bools = new bool[current.Count + before.Count];
            before.CopyTo(bools, 0);
            current.CopyTo(bools, before.Count);
            return new BitArray(bools);
        }

        public static BitArray AppendBA(BitArray current, BitArray after)
        {
            var bools = new bool[current.Count + after.Count];
            current.CopyTo(bools, 0);
            after.CopyTo(bools, current.Count);
            return new BitArray(bools);
        }
        
        public static string fetchMessage(Bitmap bmp, int stegKey)
        {
            int read_count = 0;
            bool stop = false;
            BitArray length_bit_array = new BitArray(((VarStore.Global.header_bytes_length * 8) * 5));

            bool LSB_R = false, LSB_G = false, LSB_B = false;

            Boolean[,] randPerm = new Boolean[bmp.Height, bmp.Width];

            var permutation = new Permutation(stegKey, bmp.Width, bmp.Height);

            /* Fetching header */
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    CoOrds place = permutation.NextPair();
                    Color current_pixel = bmp.GetPixel(place.j, place.i);

                    LSB_R = (current_pixel.R & (1 << 1 - 1)) != 0;
                    LSB_G = (current_pixel.G & (1 << 1 - 1)) != 0;
                    LSB_B = (current_pixel.B & (1 << 1 - 1)) != 0;

                    length_bit_array.Set(read_count, (LSB_R ^ LSB_B));
                    read_count++;
                    length_bit_array.Set(read_count, (LSB_G ^ LSB_B));

                    if ((read_count + 1) < ((VarStore.Global.header_bytes_length * 8) * 5))
                    {
                        read_count++;
                    }
                    else
                    {
                        stop = true;
                        break;
                    }
                }

                if (stop)
                    break;
            }


            /* Getting length of message bits */
            int[] array = new int[1];

            /* Decode length bit array */
            BitArray decode_length_bit_array = Correction.Decode(length_bit_array);
            decode_length_bit_array.CopyTo(array, 0);
            int message_length = array[0] * 8 * 5; // how many bits for message


            stop = false;
            read_count = 0;

            try
            {
                BitArray message_bit_array = new BitArray(message_length);

                var randomGeneratorNew = new Random(stegKey);
                Boolean[,] randPermNew = new Boolean[bmp.Height, bmp.Width];


                /* Fetching message */
                for (int i = 0; i < bmp.Height; i++)
                {
                    for (int j = 0; j < bmp.Width; j++)
                    {
                        CoOrds place = permutation.NextPair();
                        Color current_pixel = bmp.GetPixel(place.j, place.i);

                        LSB_R = (current_pixel.R & (1 << 1 - 1)) != 0;
                        LSB_G = (current_pixel.G & (1 << 1 - 1)) != 0;
                        LSB_B = (current_pixel.B & (1 << 1 - 1)) != 0;

                        message_bit_array.Set(read_count, (LSB_R ^ LSB_B));
                        read_count++;
                        message_bit_array.Set(read_count, (LSB_G ^ LSB_B));

                        if ((read_count + 1) < message_length)
                        {
                            read_count++;
                        }
                        else
                        {
                            stop = true;
                            break;
                        }

                    }

                    if (stop)
                        break;
                }

                byte[] bytes_message = new byte[(message_length / 5) / 8];

                /* Decoding bits */
                BitArray message_decode_bits = Correction.Decode(message_bit_array);
                message_decode_bits.CopyTo(bytes_message, 0);

                var str = Encoding.UTF8.GetString(bytes_message);

                return str;

            } catch (Exception exception)
            {
                MessageBox.Show("Decryption fail.", "Error occurred!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return "";
            }
        }

        public static Bitmap embedMessage(Bitmap bmp, string message, int stegKey)
        {
            var randomGenerator = new Random(stegKey);

            byte[] length_byte_array = new byte[VarStore.Global.header_bytes_length];
            length_byte_array = BitConverter.GetBytes(message.Length);

            /* Concatenate length info with message */
            BitArray message_length_bits = new BitArray(length_byte_array);
            BitArray message_text_bits = new BitArray(Encoding.UTF8.GetBytes(message));
            BitArray message_content_bits = PrependBA(message_text_bits, message_length_bits);

            /* Encoding bits */
            BitArray message_encode_bits = Correction.Encode(message_content_bits);

            BitArray message_decode_bits = Correction.Decode(message_encode_bits);

            bool stop = false;
            bool[] bit_pair = new bool[2];

            bool LSB_R = false, LSB_G = false, LSB_B = false;
            int R = 0, G = 0, B = 0;
            int msg_bit_index = 0;
            int i, j;

            var permutation = new Permutation(stegKey, bmp.Width, bmp.Height);

            for (i = 0; i < bmp.Height; i++)
            {
                for (j = 0; j < bmp.Width; j++)
                {
                    CoOrds place = permutation.NextPair();
                    Color current_pixel = bmp.GetPixel(place.j, place.i);

                    R = current_pixel.R;
                    G = current_pixel.G;
                    B = current_pixel.B;

                    LSB_R = (R & (1 << 1 - 1)) != 0;
                    LSB_G = (G & (1 << 1 - 1)) != 0;
                    LSB_B = (B & (1 << 1 - 1)) != 0;

                    bit_pair[0] = message_encode_bits.Get(msg_bit_index);
                    msg_bit_index++;
                    bit_pair[1] = message_encode_bits.Get(msg_bit_index);

                    if (bit_pair[0] != (LSB_R ^ LSB_B) && bit_pair[1] == (LSB_G ^ LSB_B))
                    {
                        R ^= (byte)(1 << 0);
                    }

                    if (bit_pair[0] == (LSB_R ^ LSB_B) && bit_pair[1] != (LSB_G ^ LSB_B))
                    {
                        G ^= (byte)(1 << 0);
                    }

                    if (bit_pair[0] != (LSB_R ^ LSB_B) && bit_pair[1] != (LSB_G ^ LSB_B))
                    {
                        B ^= (byte)(1 << 0);
                    }
                        

                    bmp.SetPixel(place.j, place.i, Color.FromArgb(R, G, B));

                    if ((msg_bit_index + 1) < message_encode_bits.Length)
                    {
                        msg_bit_index++;
                    } else
                    {
                        stop = true;
                        break;
                    }

                    if (stop)
                        break;
                }

                if (stop)
                    break;
            }

            return bmp;
        }
    }
}
