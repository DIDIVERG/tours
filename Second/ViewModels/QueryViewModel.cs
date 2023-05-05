using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using Microsoft.Win32;
using Npgsql;

namespace Second.ViewModels;

public class QueryViewModel : INotifyPropertyChanged
{
    private readonly string connectionString;
    private string queryText;
    private DataTable results;
    public AsyncRelayCommand ExecuteCommand { get; }
    public AsyncRelayCommand ExportXmlWriterCommand { get; }
    public AsyncRelayCommand ImportXmlReaderCommand { get; }
    public AsyncRelayCommand ExportXmlDocumentCommand { get; }
    public AsyncRelayCommand ImportXmlDocumentCommand { get; }
    public QueryViewModel(string connectionString)
    {
        this.connectionString = connectionString;
        ImportXmlDocumentCommand = new AsyncRelayCommand(ImportXmlDocument);
        ExportXmlDocumentCommand = new AsyncRelayCommand(ExportXmlDocument);
        ImportXmlReaderCommand = new AsyncRelayCommand(ImportXmlReader);
        ExportXmlWriterCommand = new AsyncRelayCommand(ExportXmlWriter);
        ExecuteCommand = new AsyncRelayCommand(Execute);
    }
    private async Task ExportXmlWriter()
    {
        var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "Xml files (*.xml)|*.xml";
        if (saveFileDialog.ShowDialog() == true)
        {
            try
            {
                using var xmlWriter = XmlWriter.Create(saveFileDialog.FileName);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Results");
                foreach (DataRow row in Results.Rows)
                {
                    xmlWriter.WriteStartElement("Result");
                    foreach (DataColumn column in Results.Columns)
                    {
                        xmlWriter.WriteElementString(column.ColumnName, row[column.ColumnName].ToString());
                    }
                    xmlWriter.WriteEndElement(); // закрытие тега Result
                }
                xmlWriter.WriteEndElement(); // закрытие тега Results
                xmlWriter.WriteEndDocument();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }

    private async Task ExportXmlDocument()
    {
        var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "Xml files (*.xml)|*.xml";
        if (saveFileDialog.ShowDialog() == true)
        {
            try
            {
                var xmlDoc = new XmlDocument();
                var root = xmlDoc.CreateElement("Results");
                xmlDoc.AppendChild(root);

                foreach (DataRow row in Results.Rows)
                {
                    var result = xmlDoc.CreateElement("Result");
                    root.AppendChild(result);

                    foreach (DataColumn column in Results.Columns)
                    {
                        var attribute = xmlDoc.CreateAttribute(column.ColumnName);
                        attribute.Value = row[column].ToString();
                        result.Attributes.Append(attribute);
                    }
                }

                xmlDoc.Save(saveFileDialog.FileName);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }

    private async Task ImportXmlDocument()
    {
        var openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Xml files (*.xml)|*.xml";
        if (openFileDialog.ShowDialog() == true)
        {
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(openFileDialog.FileName);
                var dataSet = new DataSet();
                dataSet.ReadXml(new XmlNodeReader(xmlDoc));
                Results = dataSet.Tables[0];
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
    private async Task ImportXmlReader()
    {
        var openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Xml files (*.xml)|*.xml";
        if (openFileDialog.ShowDialog() == true)
        {
            try
            {
                using var xmlReader = XmlReader.Create(openFileDialog.FileName);
                var dataSet = new DataSet();
                dataSet.ReadXml(xmlReader);
                Results = dataSet.Tables[0];
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
    
    public string QueryText
    {
        get => queryText;
        set
        {
            queryText = value;
            OnPropertyChanged(nameof(QueryText));
        }
    }

    public DataTable Results
    {
        get => results;
        set
        {
            results = value;
            OnPropertyChanged(nameof(Results));
        }
    }
    
   
    private async Task Execute()
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            using var adapter = new NpgsqlDataAdapter(queryText, connection);
            var table = new DataTable();
            adapter.Fill(table);
            Results = table;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
       
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}