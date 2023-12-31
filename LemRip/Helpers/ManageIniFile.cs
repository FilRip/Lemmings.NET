using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

using Microsoft.Xna.Framework;

namespace Lemmings.NET.Helpers;

public class ManageIniFile : IDisposable
{
    private List<string> _commentary = [";", "#", "//"];

    private StreamReader _configFile;
    private string _currentFileName = "";
    private string[] _section;
    private string _currentSectionName = "";
    private Encoding _currentEncoding;

    public static ManageIniFile OpenIniFile(string filename)
    {
        ManageIniFile result = new();
        result.OpenIni(filename);
        return result;
    }

    public static ManageIniFile OpenIniFile(string filename, Encoding encoding)
    {
        ManageIniFile result = new();
        result.OpenIni(filename, encoding);
        return result;
    }

    public ManageIniFile() : base() { }

    public ManageIniFile(string filename) : base()
    {
        OpenIni(filename);
    }

    public ManageIniFile(string filename, Encoding encoding) : base()
    {
        OpenIni(filename, encoding);
    }

    public bool OpenIni(string filename, Encoding encoding)
    {
        if (string.IsNullOrWhiteSpace(filename))
            return false;

        try
        {
            long size = new FileInfo(filename).Length;
            if (size > int.MaxValue)
                return false;
            if (size > 0)
            {
                _configFile = new StreamReader(filename, encoding, false, (int)size);
                _currentFileName = filename;
                _currentEncoding = encoding;
                return true;
            }
        }
        catch { /* Ignore errors */ }
        return false;
    }

    public bool OpenIni(string filename)
    {
        return OpenIni(filename, Encoding.UTF8);
    }

    public bool CreateIni(string filename)
    {
        return CreateIni(filename, Encoding.UTF8);
    }

    public bool CreateIni(string filename, Encoding encoding)
    {
        if (File.Exists(filename))
            return false;
        try
        {
            File.AppendAllText(filename, " ", encoding);
            return OpenIni(filename, encoding);
        }
        catch { /* Ignore errors */ }
        return false;
    }

    public void Close()
    {
        _configFile?.Close();
        _configFile = null;
        _currentFileName = "";
        _section = null;
        _currentSectionName = "";
    }

    public bool WriteString(string sectionName, string paramName, string value)
    {
        if (string.IsNullOrWhiteSpace(_currentFileName))
            return false;

        try
        {
            if (File.Exists(_currentFileName))
            {
                string filename = _currentFileName;
                Close();
                _currentFileName = filename;
                string[] lines = File.ReadAllLines(_currentFileName);
                bool sectionFound = false;
                bool paramFound = false;
                int lastSectionLine = 0;
                for (int numLine = 0; numLine < lines.Length; numLine++)
                {
                    if (lines[numLine].Trim().StartsWith($"[{sectionName}]", StringComparison.InvariantCultureIgnoreCase))
                        sectionFound = true;
                    else
                    {
                        if (sectionFound)
                        {
                            if (lines[numLine].Trim().StartsWith($"{paramName}=", StringComparison.InvariantCultureIgnoreCase))
                            {
                                paramFound = true;
                                lines[numLine] = $"{paramName}={value}";
                                break;
                            }
                            if (lines[numLine].Trim().StartsWith('['))
                                break;
                            lastSectionLine = numLine;
                        }
                    }
                }

                if (!paramFound)
                {
                    if (sectionFound)
                    {
                        while (lines[lastSectionLine].Trim() == "" && lastSectionLine > 0)
                            lastSectionLine--;
                        lines = lines.Insert($"{paramName}={value}", lastSectionLine + 1);
                    }
                    else
                    {
                        lines = lines.Add($"[{sectionName}]");
                        lines = lines.Add($"{paramName}={value}");
                    }
                }
                if (string.IsNullOrWhiteSpace(lines[0]))
                    lines = lines.RemoveAt(0);
                File.Delete(_currentFileName);
                File.AppendAllLines(_currentFileName, lines, _currentEncoding);
                return true;
            }
        }
        catch { /* Ignore errors */ }
        return false;
    }

    public string ReadString(string sectionName, string paramName)
    {
        if (string.IsNullOrWhiteSpace(_currentFileName))
            return "";

        return ReadSection(sectionName, paramName);
    }

    public bool ReadBoolean(string sectionName, string paramName)
    {
        if (!string.IsNullOrWhiteSpace(_currentFileName))
        {
            string result = ReadString(sectionName, paramName);
            if (!string.IsNullOrWhiteSpace(result) &&
                ((result.Trim().Equals("true", StringComparison.OrdinalIgnoreCase)) ||
                (result.Trim().Equals("yes", StringComparison.OrdinalIgnoreCase)) ||
                (result == "1")))

                return true;
        }
        return false;
    }

    public int ReadInteger(string sectionName, string paramName)
    {
        if (!string.IsNullOrWhiteSpace(_currentFileName))
        {
            string result = ReadString(sectionName, paramName);
            if (!string.IsNullOrWhiteSpace(result) && int.TryParse(result, out int convert))
                return convert;
        }
        return 0;
    }

    public long ReadLong(string sectionName, string paramName)
    {
        if (!string.IsNullOrWhiteSpace(_currentFileName))
        {
            string result = ReadString(sectionName, paramName);
            if (!string.IsNullOrWhiteSpace(result) && long.TryParse(result, out long convert))
                return convert;
        }
        return 0;
    }

    public double ReadDouble(string sectionName, string paramName)
    {
        if (!string.IsNullOrWhiteSpace(_currentFileName))
        {
            string result = ReadString(sectionName, paramName);
            if (!string.IsNullOrWhiteSpace(result) && double.TryParse(result, CultureInfo.InvariantCulture, out double convert))
                return convert;
        }
        return 0d;
    }

    public float ReadFloat(string sectionName, string paramName)
    {
        if (!string.IsNullOrWhiteSpace(_currentFileName))
        {
            string result = ReadString(sectionName, paramName);
            if (!string.IsNullOrWhiteSpace(result) && float.TryParse(result, CultureInfo.InvariantCulture, out float convert))
                return convert;
        }
        return 0f;
    }

    public DateTime? ReadDateTime(string nomSection, string nomParam)
    {
        if (!string.IsNullOrWhiteSpace(_currentFileName))
        {
            string result = ReadString(nomSection, nomParam);
            if (!string.IsNullOrWhiteSpace(result) && DateTime.TryParse(result, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out DateTime convert))
                return convert;
        }
        return null;
    }

    public Color ReadColor(string sectionName, string paramName)
    {
        if (!string.IsNullOrWhiteSpace(_currentFileName))
        {
            string result = ReadString(sectionName, paramName);
            if (!string.IsNullOrWhiteSpace(result))
            {
                string[] splitter = result.Replace("(", "").Replace(")", "").Split(',');
                return new Color(byte.Parse(splitter[0]), byte.Parse(splitter[1]), byte.Parse(splitter[2]), byte.Parse(splitter[3]));
            }
        }

        return Color.White;
    }

    public Vector2 ReadVector2(string sectionName, string paramName)
    {
        if (!string.IsNullOrWhiteSpace(_currentFileName))
        {
            string result = ReadString(sectionName, paramName);
            if (!string.IsNullOrWhiteSpace(result))
            {
                string[] splitter = result.Replace("(", "").Replace(")", "").Split(",");
                return new Vector2(float.Parse(splitter[0], CultureInfo.InvariantCulture), float.Parse(splitter[1], CultureInfo.InvariantCulture));
            }
        }
        return Vector2.Zero;
    }

    public Vector3 ReadVector3(string sectionName, string paramName)
    {
        if (!string.IsNullOrWhiteSpace(_currentFileName))
        {
            string result = ReadString(sectionName, paramName);
            if (!string.IsNullOrWhiteSpace(result))
            {
                string[] splitter = result.Replace("(", "").Replace(")", "").Split(",");
                return new Vector3(float.Parse(splitter[0], CultureInfo.InvariantCulture), float.Parse(splitter[1], CultureInfo.InvariantCulture), float.Parse(splitter[2], CultureInfo.InvariantCulture));
            }
        }
        return Vector3.Zero;
    }

    public T ReadEnum<T>(string sectionName, string paramName) where T : struct
    {
        if (!string.IsNullOrWhiteSpace(_currentFileName))
        {
            string result = ReadString(sectionName, paramName);
            if (!string.IsNullOrWhiteSpace(result) &&
                Enum.TryParse(result, out T cast))
            {
                return cast;
            }
        }
        return default;
    }

    public Rectangle ReadRectangle(string sectionName, string paramName)
    {
        if (!string.IsNullOrWhiteSpace(_currentFileName))
        {
            string result = ReadString(sectionName, paramName);
            if (!string.IsNullOrWhiteSpace(result))
            {
                string[] splitter = result.Replace("(", "").Replace(")", "").Split(',');
                return new Rectangle(int.Parse(splitter[0]), int.Parse(splitter[1]), int.Parse(splitter[2]), int.Parse(splitter[3]));
            }
        }
        return default;
    }

    public string CurrentFileName
    {
        get { return _currentFileName; }
    }

    private string ReadStartOfSection(string param)
    {
        if (!string.IsNullOrWhiteSpace(_currentFileName))
        {
            _configFile.DiscardBufferedData();
            _configFile.BaseStream.Seek(0, SeekOrigin.Begin);
            string line;
            while (true)
            {
                line = _configFile.ReadLine();
                if (line == null)
                    break;
                if (line.Trim().Equals(param.Trim(), StringComparison.OrdinalIgnoreCase))
                    return line;
            }
        }
        return "";
    }

    private string ReadSection(string sectionName, string paramName)
    {
        string result = "";
        bool commentary;

        if (!string.IsNullOrWhiteSpace(_currentFileName) && (sectionName != null) && (sectionName.Trim() != "") && (paramName != null) && (paramName.Trim() != ""))
        {
            if (!_currentSectionName.Equals(sectionName.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                string currentLine;
                List<string> lines = [];

                currentLine = ReadStartOfSection("[" + sectionName + "]");
                if ((currentLine != null) && (currentLine.Trim() != ""))
                {
                    while (true)
                    {
                        currentLine = _configFile.ReadLine();
                        if ((currentLine == null) || (currentLine.Trim().StartsWith('[')))
                            break;
                        commentary = false;
                        if (_commentary?.Count > 0 &&
                            _commentary.Exists(c => currentLine.Trim().StartsWith(c)))
                        {
                            commentary = true;
                        }
                        if (!commentary)
                            lines.Add(currentLine);
                    }
                    _section = lines.ToArray();
                    _currentSectionName = sectionName.Trim().ToLower();
                }
            }

            if ((_section != null) && (_section.Length > 0))
            {
                for (int j = 0; j < _section.Length; j++)
                {
                    if (_section[j].Trim().StartsWith(paramName.Trim() + "=", StringComparison.OrdinalIgnoreCase))
                    {
                        int pos = _section[j].IndexOf('=') + 1;
                        result = _section[j][pos..].Trim();
                        break;
                    }
                }
            }
        }
        return result;
    }

    public string[] SectionContent
    {
        get { return _section; }
    }

    public string NameOfCurrentSection
    {
        get { return _currentSectionName; }
    }

    public void SetCommentary(List<string> newCommentaryChars)
    {
        _commentary = newCommentaryChars;
    }

    public void AddCommentary(string commentaryToAdd)
    {
        if ((_commentary != null) && (!_commentary.Contains(commentaryToAdd)))
        {
            _commentary.Add(commentaryToAdd);
        }
    }

    public void RemoveCommentary(string commentaryToRemove)
    {
        if ((_commentary != null) && (_commentary.Contains(commentaryToRemove)))
        {
            _commentary.Remove(commentaryToRemove);
        }
    }

    public int NumberOfSection(string sectionName)
    {
        int nbSections = 0;
        if (!string.IsNullOrWhiteSpace(_currentFileName))
        {
            _configFile.DiscardBufferedData();
            _configFile.BaseStream.Seek(0, SeekOrigin.Begin);
            string line;
            while (true)
            {
                line = _configFile.ReadLine();
                if (line == null)
                    break;
                if (line.Trim().StartsWith("[" + sectionName.Trim(), StringComparison.OrdinalIgnoreCase))
                    nbSections++;
            }
        }
        return nbSections;
    }

    #region IDisposable Support

    public bool IsDisposed
    {
        get { return disposedValue; }
    }

    private bool disposedValue = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _configFile?.Dispose();
                if ((_section != null) && (_section.Length > 0))
                {
                    Array.Clear(_section, 0, _section.Length);
                    Array.Resize(ref _section, 0);
                }
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion
}
