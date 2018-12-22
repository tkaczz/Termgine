﻿using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace Termgine {
  class Program {
    private static void Main(string[] args) {
      var mario = 
      "      ██████████        \n" +
      "    ██████████████████  \n" +
      "    ██████████████      \n" +
      "  ████████████████████  \n" +
      "  ██████████████████████\n" +
      "  ████████████████████  \n" +
      "      ██████████████    \n" +
      "    ██████████████      \n" +
      "  ████████████████████  \n" +
      "████████████████████████\n" +
      "████████████████████████\n" +
      "████████████████████████\n" +
      "████████████████████████\n" +
      "    ██████    ███████   \n" +
      "  ██████         █████  \n" +
      "████████         ███████\n";

      var marioColorMask = 
      "      1111111111        \n" +
      "    111111111111111111  \n" +
      "    88888833338833      \n" +
      "  88338833333388333333  \n" +
      "  8833888833333388333333\n" +
      "  88883333333388888888  \n" +
      "      33333333333333    \n" +
      "    11114411111111      \n" +
      "  11111144111144111111  \n" +
      "111111114444444411111111\n" +
      "333311443344443344113333\n" +
      "333333444444444444333333\n" +
      "333344444444444444443333\n" +
      "    444444    4444444   \n" +
      "  888888         88888  \n" +
      "88888888         8888888\n";

      // Scene setup
      var display = new Display(Display.MaxWidth/2, Display.MaxHeight/2);
      var image = new Image(new Vector2(10, 10), mario, marioColorMask);
      var scene = new Scene();
      scene.AddObject(image);
      display.HideCursor();
      display.AddScene(scene);
      display.Show();
      display.WaitForKey();
      while (true) {
        Thread.Sleep(100/10);
        // display.WaitForKey();
        display.CurrentScene.GameObjects[0].Position.X++;
        display.Refresh();
      }
    }
  }
}
