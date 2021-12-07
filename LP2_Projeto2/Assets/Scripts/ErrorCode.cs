/// <summary>
/// Codes that define which type of error message is displayed to the user.
/// </summary>
public enum ErrorCode
{
   /// <summary>
   /// Code to display a message when the given map size is invalid.
   /// </summary>
   InvalidMapSize,

   /// <summary>
   /// Code to display a message when no map size was given in the file's first
   /// line.
   /// </summary>
   NoMapSize,

   /// <summary>
   /// Code to display a message when a word that was supposed to represent a
   /// terrain does not exist in the array containing all the possible terrains.
   /// </summary>
   UnknownTerrain,

   /// <summary>
   /// Code to display a message when a word that was supposed to represent a
   /// resource does not exist in the array containing all the possible resources.
   /// </summary>
   UnknownResource
}
