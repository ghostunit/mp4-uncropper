﻿namespace Mp4UnCropper
{
  internal class ModifiedFile : BinaryFile
  {
    private byte[] originalFile;
    private byte[] oldData;
    private byte[] newData;

    internal ModifiedFile(byte[] originalFile, byte[] oldData, byte[] newData, string destinationFilename)
    {
      this.originalFile = originalFile;
      this.oldData = oldData;
      this.newData = newData;
      Path = destinationFilename;
      Result = FileResult.Undefined;
      ReplaceBytes();
    }

    private void ReplaceBytes()
    {
      Bytes = originalFile;

      if (Bytes == null || Bytes.Length == 0)
      {
        Bytes = new byte[0];
        Result = FileResult.NoFileToModify;
      }

      PatternMatch patternMatch = new PatternMatch(oldData, Bytes);
      if (!patternMatch.Success)
      {
        Bytes = new byte[0];
        Result = FileResult.PatternNotFound;
      }
      else
      {
        Result = FileResult.Success;
        Bytes[patternMatch.Index] = newData[0];
        Bytes[patternMatch.Index + 1] = newData[1];
        Bytes[patternMatch.Index + 2] = newData[2];
        Bytes[patternMatch.Index + 3] = newData[3];
      }
    }

  }
}