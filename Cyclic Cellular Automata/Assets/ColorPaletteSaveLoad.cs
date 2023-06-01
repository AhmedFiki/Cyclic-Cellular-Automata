using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

[Serializable]
public class ColorPaletteSaveLoad
{
    [SerializeField]
    public static ColorPaletteSaveLoad Instance { get; private set; }

    [SerializeField]
    private ColorPaletteSerializable colorPalette; // The ColorPalette object to save/load
   
    [SerializeField]
    private static string savePath; // Path to save the color palette file



    public static void Initialize()
    {
        if (Instance == null)
        {
            Instance = new ColorPaletteSaveLoad();
        }

        savePath = Application.persistentDataPath + "/colorpalettes/";
        Directory.CreateDirectory(savePath); // Create the save directory if it doesn't exist
    }

   
    public void SaveColorPalette(ColorPalette paletteObject,string paletteName)
    {

        colorPalette = paletteObject.ToSerializable();
        string filePath = GetFilePath(paletteName);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = File.Create(filePath);

        // Convert the ColorPalette object to binary format
        byte[] paletteData = SerializeColorPalette(colorPalette);

        // Write the serialized data to the file
        fileStream.Write(paletteData, 0, paletteData.Length);
        fileStream.Close();

        Debug.Log("ColorPalette saved with name: " + paletteName);
    }

    /*public ColorPalette LoadColorPalette(string paletteName)
    {
        string filePath = GetFilePath(paletteName);

        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Open(filePath, FileMode.Open);

            // Read the serialized data from the file
            byte[] paletteData = new byte[fileStream.Length];
            fileStream.Read(paletteData, 0, (int)fileStream.Length);
            fileStream.Close();

            // Convert the binary data back to ColorPalette object
            colorPalette = DeserializeColorPalette(paletteData);

            Debug.Log("ColorPalette loaded with name: " + paletteName);
            ColorPalette c = new ColorPalette();
            c.SerializableToSO(colorPalette);
            return c;
        }
        else
        {
            Debug.Log("ColorPalette with name " + paletteName + " not found!");
            return null;
        }
    }*/
    public ColorPalette[] LoadAllColorPalettes()
    {
        DirectoryInfo directory = new DirectoryInfo(savePath);
        FileInfo[] files = directory.GetFiles("*.dat"); // Assuming the saved files have the extension ".dat"

        List<ColorPalette> colorPalettes = new List<ColorPalette>();

        foreach (FileInfo file in files)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Open(file.FullName, FileMode.Open);

            // Read the serialized data from the file
            byte[] paletteData = new byte[fileStream.Length];
            fileStream.Read(paletteData, 0, (int)fileStream.Length);
            fileStream.Close();

            // Convert the binary data back to ColorPaletteSerializable object
            ColorPaletteSerializable colorPaletteSerializable = DeserializeColorPalette(paletteData);

            // Convert ColorPaletteSerializable to ColorPalette
            ColorPalette colorPalette = new ColorPalette();
            colorPalette.SerializableToSO(colorPaletteSerializable);

            colorPalettes.Add(colorPalette);
        }

        Debug.Log("Loaded " + colorPalettes.Count + " ColorPalettes from path: " + savePath);
        return colorPalettes.ToArray();
    }

    private byte[] SerializeColorPalette(ColorPaletteSerializable palette)
    {
        MemoryStream memoryStream = new MemoryStream();
        BinaryFormatter formatter = new BinaryFormatter();

        // Serialize the ColorPalette object to the memory stream
        formatter.Serialize(memoryStream, palette);

        return memoryStream.ToArray();
    }

    private ColorPaletteSerializable DeserializeColorPalette(byte[] data)
    {
        MemoryStream memoryStream = new MemoryStream(data);
        BinaryFormatter formatter = new BinaryFormatter();

        // Deserialize the ColorPalette object from the memory stream
        ColorPaletteSerializable palette = (ColorPaletteSerializable)formatter.Deserialize(memoryStream);

        return palette;
    }

    private string GetFilePath(string paletteName)
    {
        return savePath + paletteName + ".dat";
    }
}

