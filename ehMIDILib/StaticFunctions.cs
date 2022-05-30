using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ehMIDILib
{
    public static class StaticFunctions
    {
        public static string GetString(int magic)
        {
            //어떤 파일이 무슨 파일 형식임을 알려주기 위한 4 byte 크기의 부분.
            byte[] data= BitConverter.GetBytes(magic);
            ASCIIEncoding en = new ASCIIEncoding();
            return en.GetString(data);
        }


        public static int ConvertHostOrder(int data)
        {
            return IPAddress.NetworkToHostOrder(data);
        }

        public static int ReadDeltaTime(byte[] buffer, ref int offset)
        {
            int time = 0;
            byte b;
            do
            {
                b = buffer[offset];
                offset++;
                time = (time << 7) | (b & 0x7F); // 7 bit만 추출을 함.
            } while (b > 0x7F); //가장 첫 번째 bit가 1인 경우.

            return time;
        }

        public static string GetString(byte[] data)
        {
            Encoding en = Encoding.Default;
            return en.GetString(data);
        }

        public static short ConvertHostOrder(short data)
        {
            return IPAddress.NetworkToHostOrder(data);
        }

        public static short ConvertHostOrderS(byte[] data, int offset)
        {
            return ConvertHostOrder(BitConverter.ToInt16(data, offset));
        }

        public static string HexaString(byte[] buffer)
        {
            string str = "";
            foreach(byte d in buffer)
            {
                str += string.Format("{0:X2} ", d);
            }

            return str;
        }


        public static string[] noteNames = new string[]
            {   "C",
                "C#",
                "D",
                "D#",
                "E",
                "F",
                "F#",
                "G",
                "G#",
                "A",
                "A#",
                "B"
            };
        public static string GetNoteName(int num)
        {
            return string.Format("{0}{1}", noteNames[num % 12], num / 12);
        }

        static string[] control_str = new string[]
        {  "0 Bank Select",
            "1 Modulation Wheel",
            "2 Breath controller",
            "3 Undefined",
            "4 Foot Pedal",
            "5 Portamento Time",
            "6 Data Entry",
            "7 Volume",
            "8 Balance",
            "9 Undefined",
            "10 Pan position",
            "11 Expression",
            "12 Effect Control 1",
            "13 Effect Control 2",
            "14 Undefined",
            "15 Undefined",
            "16 Ribbon Controller or General Purpose Slider 1",
            "17 Knob 1 or General Purpose Slider 2",
            "18 General Purpose Slider 3",
            "19 Knob 2 General Purpose Slider 4",
            "20 Knob 3 or Undefined",
            "21 Knob 4 or Undefined",
            "22 Undefined",
            "23 Undefined",
            "24 Undefined",
            "25 Undefined",
            "26 Undefined",
            "27 Undefined",
            "28 Undefined",
            "29 Undefined",
            "30 Undefined",
            "31 Undefined",
            "32 Bank Select",
            "33 Modulation Wheel",
            "34 Breath controller",
            "35 Undefined",
            "36 Foot Pedal",
            "37 Portamento Time",
            "38 Data Entry",
            "39 Volume",
            "40 Balance",
            "41 Undefined",
            "42 Pan position",
            "43 Expression",
            "44 Effect Control 1",
            "45 Effect Control 2",
            "46 Undefined",
            "47 Undefined",
            "48 Undefined",
            "49 Undefined",
            "50 Undefined",
            "51 Undefined",
            "52 Undefined",
            "53 Undefined",
            "54 Undefined",
            "55 Undefined",
            "56 Undefined",
            "57 Undefined",
            "58 Undefined",
            "59 Undefined",
            "60 Undefined",
            "61 Undefined",
            "62 Undefined",
            "63 Undefined",
            "64 Hold Pedal(on/off)",
            "65 Portamento(on/off)",
            "66 Sustenuto Pedal(on/off)",
            "67 Soft Pedal(on/off)",
            "68 Legato Pedal(on/off)",
            "69 Hold 2 Pedal(on/off)",
            "70 Sound Variation",
            "71 Resonance(aka Timbre)",
            "72 Sound Release Time",
            "73 Sound Attack Time",
            "74 Frequency Cutoff(aka Brightness)",
            "75 Sound Control 6",
            "76 Sound Control 7",
            "77 Sound Control 8",
            "78 Sound Control 9",
            "79 Sound Control 10",
            "80 Decay or General Purpose Button 1 (on/off)",
            "81 Hi Pass Filter Frequency or General Purpose Button 2 (on/off)",
            "82 General Purpose Button 3 (on/off) Roland Tone level 3",
            "83 General Purpose Button 4 (on/off) Roland Tone level 4",
            "84 Undefined",
            "85 Undefined",
            "86 Undefined",
            "87 Undefined",
            "88 Undefined",
            "89 Undefined",
            "90 Undefined",
            "91 Reverb Level",
            "92 Tremolo Level",
            "93 Chorus Level",
            "94 Celeste Level or Detune",
            "95 Phaser Level",
            "96 Data Button increment",
            "97 Data Button decrement",
            "98 Non-registered Parameter",
            "99 Non-registered Parameter",
            "100 Registered Parameter",
            "101 Registered Parameter",
            "102 Undefined",
            "103 Undefined",
            "104 Undefined",
            "105 Undefined",
            "106 Undefined",
            "107 Undefined",
            "108 Undefined",
            "109 Undefined",
            "110 Undefined",
            "111 Undefined",
            "112 Undefined",
            "113 Undefined",
            "114 Undefined",
            "115 Undefined",
            "116 Undefined",
            "117 Undefined",
            "118 Undefined",
            "119 Undefined",
            "120 All Sound Off",
            "121 All Controllers Off",
            "122 Local Keyboard(on/off)",
            "123 All Notes Off",
            "124 Omni Mode Off",
            "125 Omni Mode On",
            "126 Mono Operation",
            "127 Poly Operation"
        };
        public static string GetControlStr(byte sdata)
        {
            return control_str[sdata & 0x7F];
        }

        public static string GetInstrumentName(int insnum)
        {
            switch (insnum)
            {
                //PIANO
                case 0: return "Acoustic Grand";
                case 1: return "Bright Acoustic";
                case 2: return "Electric Grand";
                case 3: return "Honky-Tonk";
                case 4: return "Electric Piano 1";
                case 5: return "Electric Piano 2";
                case 6: return "Harpsichord";
                case 7: return "Clavinet";
                //CHROMATIC
                case 8: return "celesta";
                case 9: return "Glockenspiel";
                case 10: return "Music Box";
                case 11: return "Vibraphone";
                case 12: return "Marimba";
                case 13: return "Xylophone";
                case 14: return "Tubular Bells";
                case 15: return "Dulcimer";
                //ORGAN
                case 16: return "Drawbar Organ";
                case 17: return "Percussive Organ";
                case 18: return "Rock Organ";
                case 19: return "Church Organ";
                case 20: return "Reed Organ";
                case 21: return "Accordian";
                case 22: return "Harmonica";
                case 23: return "Tango Accordian";
                //GUITAR
                case 24: return "Acoustic Guitar(nylon)";
                case 25: return "Acoustic Guitar(steel)";
                case 26: return "Electric Guitar(jazz)";
                case 27: return "Electric Guitar(clean)";
                case 28: return "Electric Guitar(muted)";
                case 29: return "Overdriven Guitar";
                case 30: return "Distortion Guitar";
                case 31: return "Guitar Harmonics";
                //BASS
                case 32: return "Acoustic Bass";
                case 33: return "Electric Bass (finger)";
                case 34: return "Electric Bass (pick)";
                case 35: return "Fretless Bass";
                case 36: return "Slap Bass 1";
                case 37: return "Slap Bass 2";
                case 38: return "Synth Bass 1";
                case 39: return "Synth Bass 2";
                //STRINGS
                case 40: return "Violin";
                case 41: return "Viola";
                case 42: return "Cello";
                case 43: return "Contrabass";
                case 44: return "Tremolo Strings";
                case 45: return "Pissicato Strings";
                case 46: return "Orchestral Strings";
                case 47: return "Timpani";
                //ENSEMBLE
                case 48: return "String Ensemble 1";
                case 49: return "String Ensemble 2";
                case 50: return "SynthStrings 1";
                case 51: return "SynthStrings 2";
                case 52: return "Choir Aahs";
                case 53: return "Voice Oohs";
                case 54: return "Synth Voice";
                case 55: return "Orchestra Hit";
                //BRASS
                case 56: return "Trumpet";
                case 57: return "Trombone";
                case 58: return "Tuba";
                case 59: return "Muted Trumpet";
                case 60: return "French Horn";
                case 61: return "Brass Section";
                case 62: return "SynthBrass 1";
                case 63: return "SynthBrass 2";
                //REED
                case 64: return "Soprano Sax";
                case 65: return "Alto Sax";
                case 66: return "Tenor Sax";
                case 67: return "Baritone Sax";
                case 68: return "Oboe";
                case 69: return "English Horn";
                case 70: return "Bassoon";
                case 71: return "Clarinet";
                //PIPE
                case 72: return "Piccolo";
                case 73: return "Flute";
                case 74: return "Recorder";
                case 75: return "Pan Flute";
                case 76: return "Blown Bottle";
                case 77: return "Skakuhachi";
                case 78: return "Whistle";
                case 79: return "Ocarina";
                //SYNTH LEAD
                case 80: return "Lead 1 (square)";
                case 81: return "Lead 2 (sawtooth)";
                case 82: return "Lead 3 (calliope)";
                case 83: return "Lead 4 (chiff)";
                case 84: return "Lead 5 (charang)";
                case 85: return "Lead 6 (voice)";
                case 86: return "Lead 7 (fifths)";
                case 87: return "Lead 8 (bass+lead)";
                //SYNYH PAD
                case 88: return "Pad 1 (new age)";
                case 89: return "Pad 2 (warm)";
                case 90: return "Pad 3 (polysynth)";
                case 91: return "Pad 4 (choir)";
                case 92: return "Pad 5 (bowed)";
                case 93: return "Pad 6 (metallic)";
                case 94: return "Pad 7 (halo)";
                case 95: return "Pad 8 (sweep)";
                //SYNTH EFFECTS
                case 96: return "FX 1 (rain)";
                case 97: return "FX 2 (soundtrack)";
                case 98: return "FX 3 (crystal)";
                case 99: return "FX 4 (atomosphere)";
                case 100: return "FX 5 (brightness)";
                case 101: return "FX 6 (goblins)";
                case 102: return "FX 7 (echoes)";
                case 103: return "FX 8 (sci-fi)";
                //ETHNIC
                case 104: return "Sitar";
                case 105: return "Banjo";
                case 106: return "Shamisen";
                case 107: return "Koto";
                case 108: return "Kalimba";
                case 109: return "Bagpipe";
                case 110: return "Fiddle";
                case 111: return "Shanai";
                //PERCUSSIVE
                case 112: return "Tinkle Bell";
                case 113: return "Agogo";
                case 114: return "Steel Drums";
                case 115: return "Woodblock";
                case 116: return "Taiko Drum";
                case 117: return "Melodic Tom";
                case 118: return "Synth Drum";
                case 119: return "Reverse Cymbal";
                //SOUND
                case 120: return "Guitar Fret Noise";
                case 121: return "Breath Noise";
                case 122: return "Seashore";
                case 123: return "Bird Tweet";
                case 124: return "Telephone Ring";
                case 125: return "Helicopter";
                case 126: return "Applause";
                case 127: return "Gunshot";
                default: return "알 수 없음";
            }
        }


    }
}
