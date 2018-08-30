using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace ConsoleBuddy
{


    public static class Util
    {
        public static string GetRandomFile(string path)
        {
            string file = null;
            if (!string.IsNullOrEmpty(path))
            {
                var extensions = new string[] { ".png", ".jpg", ".gif" };
                try
                {
                    var di = new DirectoryInfo(path);
                    var rgFiles = di.GetFiles("*.*").Where(f => extensions.Contains(f.Extension.ToLower()));
                    Random R = new Random();
                    file = rgFiles.ElementAt(R.Next(0, rgFiles.Count())).FullName;
                }
                // probably should only catch specific exceptions
                // throwable by the above methods.
                catch { }
            }
            return file;
        }



        //this function takes a function returning bool and calls it either until it succeeds or the time is up
        //use () => myfunc( v1,v2,v3) in task spot if your function takes arguments
        public static bool RetryUntilSuccessOrTimeout(Func<bool> task, TimeSpan timeSpan)
        {
            bool success = false;
            int elapsed = 0;
            while ((!success) && (elapsed < timeSpan.TotalMilliseconds))
            {
                Thread.Sleep(1000);
                elapsed += 1000;
                success = task();
            }
            return success;
        }

        //taranslates any list of objects directly to a datatable
        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

        public static string ListToString(string separator, List<object> inString)
        {
            return String.Join(separator, inString.ToArray());
        }

        public static bool MoveToFrontOfListWhere<T>(this List<T> collection, Func<T, bool> predicate)
        {
            if (collection == null || collection.Count <= 0) return false;

            int index = -1;
            for (int i = 0; i < collection.Count; i++)
            {
                T element = collection.ElementAt(i);
                if (!predicate(element)) continue;
                index = i;
                break;
            }

            if (index == -1) return false;

            T item = collection[index];
            collection[index] = collection[0];
            collection[0] = item;
            return true;
        }

        // generate random hash for unique filename
        public static string ReturnUniqueValue(string ID)
        {
            var result = default(byte[]);

            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
                {
                    writer.Write(DateTime.Now.Ticks);
                    writer.Write(ID);
                }

                stream.Position = 0;

                using (var hash = System.Security.Cryptography.SHA256.Create())
                {
                    result = hash.ComputeHash(stream);
                }
            }

            var text = new string[20];

            for (var i = 0; i < text.Length; i++)
            {
                text[i] = result[i].ToString("x2");
            }

            return string.Concat(text);
        }

        /// <summary>
        /// This will check a filename in a path for duplicates. if there is a duplicate, 
        /// it will append a number 
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="infilename"></param>
        /// <returns></returns>
        public static string IndexedFilename(string folder, string infilename)
        {
            if (!File.Exists(folder + infilename))
            {
                return infilename;
            }

            var namesplit = infilename.Split('.');
            int ix = 1;
            string filename = null;
            do
            {
                ix++;
                filename = String.Format("{0}({1}).{2}", namesplit[0], ix, namesplit[1]);
            } while (File.Exists(folder + filename));
            return filename;
        }

        public static void startFile(string filePath)
        {
            System.Diagnostics.Process.Start(filePath);
        }

        public static string ListToString(this IEnumerable<object> inString, string separator)
        {
            return String.Join(separator, inString.ToArray());
        }
        public static string ToString<T>(this ObservableCollection<T> inList) where T : class
        {
            return Util.ListToString(inList.AsEnumerable(), "\n");
        }

        //public static DataTable ToDataTable<T>(this IEnumerable<T> data)
        //{
        //    DataTable table = new DataTable();
        //    using (var reader = ObjectReader.Create(data))
        //    {
        //        table.Load(reader);
        //    }
        //    return table;
        //}

        public static int Clamp(this int value, int min, int max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

    }

    /// <summary>
    /// Example:
    /// using (new TimeIt("Outer scope"))
    ///{
    ///using (new TimeIt("Inner scope A"))
    ///{
    ///DoSomeWork("A");
    ///}
    ///using (new TimeIt("Inner scope B"))
    ///{
    ///DoSomeWork("B");
    ///}
    ///Cleanup();
    ///}
    /// </summary>
    public class TimeIt : IDisposable
    {
        private readonly string _name;
        private readonly Stopwatch _watch;
        public TimeIt(string name)
        {
            _name = name;
            _watch = Stopwatch.StartNew();
        }
        public void Dispose()
        {
            _watch.Stop();
         System.Console.WriteLine("{0} took {1}", _name, _watch.Elapsed);
        }
    }
}
