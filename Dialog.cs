using System.Windows.Forms;

namespace DialogLibrary
{
    public static class Dialog
    {
        private static readonly string[] Filters = 
        {
            "Файлы изображений (.jpg, .jpeg, .png, .bmp) | *.jpg; *.jpeg; *.png; *.bmp",
            "csv файлы | *.csv"
        };

        /// <summary>
        /// Открыть файл через диалоговое окно и выполнить функцию делегата DialogOK
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="handler"></param>
        public static void OpenFile(DataType dataType, DialogHandler handler)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true,
                Filter = Filters[(int)dataType]
            };

            if (dialog.ShowDialog() == DialogResult.OK) handler(dialog.FileName);
        }

        /// <summary>
        /// Сохранить файл через диалоговое окно и выполнить функцию делегата DialogOK
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="handler"></param>
        public static void SaveFile(DataType dataType, DialogHandler handler)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                RestoreDirectory = true,
                Filter = Filters[(int)dataType]
            };

            if (dialog.ShowDialog() == DialogResult.OK) handler(dialog.FileName);
        }
    }

    /// <summary>
    /// Делегат, содержащий функцию, которую необходимо вызвать при открытии или сохранении файла
    /// </summary>
    public delegate void DialogHandler(string filename);

    /// <summary>
    /// Разширения открываемых и сохраняемых файлов
    /// </summary>
    public enum DataType
    {
        Images,
        Csv
    }
}
