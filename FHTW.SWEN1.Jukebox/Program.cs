using FHTW.SWEN1.JukeInterfaces;
using System.Reflection;

namespace FHTW.SWEN1.Jukebox
{
    internal class Program
    {
        /// <summary>Library file.</summary>
        private const string _LIB_FILE = @"C:\home\projects\FHTW.SWEN1.Jukebox.NET\BachPlayer\bin\Debug\net6.0\BachPlayer.dll";



        /// <summary>Main entry point.</summary>
        /// <param name="args">Arguments.</param>
        static void Main(string[] args)
        {
            WithInterface();
            WithoutInterface();
        }


        /// <summary>Dynamicly loads music using a common interface.</summary>
        static void WithInterface()
        {
            Assembly asm = Assembly.LoadFile(_LIB_FILE);

            Console.WriteLine(asm.ToString());

            foreach(Type i in asm.GetTypes())
            {
                if(i.IsAssignableTo(typeof(IMusicPlayer)))
                {
                    if(!i.IsAbstract)
                    {
                        Console.WriteLine("Music is available from " + i.Name + ". Rejoice!");

                        IMusicPlayer? myMusic = (IMusicPlayer?) Activator.CreateInstance(i);

                        if(myMusic != null)
                        {
                            ((IMusicPlayer) myMusic).Play();
                        }
                    }
                }
            }
        }



        /// <summary>Dynamicly loads music without casting to an interface.</summary>
        static void WithoutInterface()
        {
            Assembly asm = Assembly.LoadFile(_LIB_FILE);

            Console.WriteLine(asm.ToString());

            foreach(Type i in asm.GetTypes())
            {
                if(i.GetInterfaces().Where(m => (m.Name == "IMusicPlayer")).Any())
                {
                    if(!i.IsAbstract)
                    {
                        Console.WriteLine("Music is available from " + i.Name + ". Rejoice!");

                        object? myMusic = Activator.CreateInstance(i);

                        if(myMusic != null)
                        {
                            i.GetMethod("Play")?.Invoke(myMusic, null);
                        }
                    }
                }
            }
        }
    }
}