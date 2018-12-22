﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Termgine {
  public class Scene {
    #region Constructor

    public Scene() {
      Height = Console.WindowHeight;
      Width =  Console.WindowWidth;
      GameObjects = new List<GameObject>();
    }

    #endregion

    #region Public variables

    public List<GameObject> GameObjects;

    public int Width {
      get => _width;

      set {
        _width = value;
        OnSizeChanged();
      }
    }

    public int Height {
      get => _height;
      set {
        _height = value;
        OnSizeChanged();
      }
    }

    public string Content {
      get {
        // TODO More efficent instead of reseting all content
        var content = new StringBuilder();
        UpdateContent();
        foreach (var line in _content){
          content.Append(new string(line) + "\n");
        }
        return content.ToString();
      }
    }

    public string ContentColors {
      get {
        var colorMask = new StringBuilder();
        UpdateColorMask();
        foreach (var line in _colorMask){
          colorMask.Append(new string(line) + "\n");
        }
        return colorMask.ToString();
      }
    }


    #endregion

    #region Public methods

    public void AddObject(GameObject o) {
      AddObjectAt(o, o.Position);
    }

    public void AddObjectAt(GameObject o, Vector2 position) {
      if (position.X > Width || position.Y > Height)
        throw new ArgumentException("Position set outside scene size");

      o.Position = position;
      GameObjects.Add(o);
      AddObjectToGlobalContent(o.Content, position);
      AddObjectColorToGlobalColorMask(o.ColorMask, position);
    }

    #endregion

    #region Private variables

    private int _width;
    private int _height;
    private char[][] _content;
    private char[][] _colorMask;

    #endregion

    #region Private methods

    private void AddObjectToGlobalContent(string gameObjectContent, Vector2 position) {
      var contentLines = gameObjectContent.Split('\n');
      for (var y = contentLines.Length - 1; y >= 0; y--) {
        var globalPositionY = y + position.Y;
        if (globalPositionY >= Height) continue;
        for (var x = 0; x < contentLines[y].Length; x++) {
          var globalPositionX = x + position.X;
          if (globalPositionX >= Width) break;
          if (contentLines[y][x] != ' ')
            _content[globalPositionY][globalPositionX] = contentLines[y][x];
        }
      }
    }

    private void AddObjectColorToGlobalColorMask(string gameObjectColorMask, Vector2 position) {
      var contentLines = gameObjectColorMask.Split('\n');
      for (var y = contentLines.Length - 1; y >= 0; y--) {
        var globalPositionY = y + position.Y;
        if (globalPositionY >= Height) continue;
        for (var x = 0; x < contentLines[y].Length; x++) {
          var globalPositionX = x + position.X;
          if (globalPositionX >= Height) break;
          if (contentLines[y][x] != ' ')
            _colorMask[globalPositionY][globalPositionX] = contentLines[y][x];
        }
      }
    }

    private void UpdateContent(){
      InitContent();
      foreach (var gameObject in GameObjects){
        AddObjectToGlobalContent(gameObject.Content, gameObject.Position);
      }
    }

    private void UpdateColorMask(){
      InitColorMask();
      foreach (var gameObject in GameObjects){
        AddObjectColorToGlobalColorMask(gameObject.ColorMask, gameObject.Position);
        }
    }


    private void OnSizeChanged() {
      InitContent();
      InitColorMask();
    }

    private void InitContent() {
      _content = new char[Height][];
      for (var i = 0; i < _content.Length; i++) {
        _content[i] = new char[Width];
        for (var index = 0; index < _content[i].Length; index++) {
          _content[i][index] = ' ';
        }
      }
    }

    private void InitColorMask() {
      _colorMask = new char[Height][];
      for (var i = 0; i < _colorMask.Length; i++) {
        _colorMask[i] = new char[Width];
        for (var index = 0; index < _colorMask[i].Length; index++) {
          _colorMask[i][index] = ' ';
        }
      }

    }

    #endregion
  }
}