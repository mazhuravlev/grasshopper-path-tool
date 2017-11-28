using System;
using System.Collections.Generic;
using System.IO;
using Grasshopper.Kernel;
using Rhino.Geometry;
using TplTest.Properties;

namespace TplTest
{
  public class PathToolComponent : GH_Component
  {
    private int _pathIn;
    private int _pathOut;

    /// <summary>
    /// Each implementation of GH_Component must provide a public 
    /// constructor without any arguments.
    /// Category represents the Tab in which the component will appear, 
    /// Subcategory the panel. If you use non-existing tab or panel names, 
    /// new tabs/panels will automatically be created.
    /// </summary>
    public PathToolComponent()
      : base("PathTool", "PathTool",
          "Create file path relative to GH document file",
          "Params", "Util")
    {
    }

    /// <summary>
    /// Registers all the input parameters for this component.
    /// </summary>
    protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
    {
      _pathIn = pManager.AddTextParameter("Rel path", "Rel path", "Relative path", GH_ParamAccess.item, "");
    }

    /// <summary>
    /// Registers all the output parameters for this component.
    /// </summary>
    protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
    {
      _pathOut = pManager.AddTextParameter("Abs path", "Abs path", "Absolute path", GH_ParamAccess.item);
    }

    /// <summary>
    /// This is the method that actually does the work.
    /// </summary>
    /// <param name="da">The DA object can be used to retrieve data from input parameters and 
    /// to store data in output parameters.</param>
    protected override void SolveInstance(IGH_DataAccess da)
    {
      var docPath = OnPingDocument().FilePath;
      if (string.IsNullOrEmpty(docPath)) throw new Exception("Document path is null or empty");
      var docDir = Path.GetDirectoryName(docPath);
      if (docDir == null) throw new Exception("Unable to get document directory name");
      var relPath = "";
      da.GetData(_pathIn, ref relPath);
      var result = Path.GetFullPath(Path.Combine(docDir, relPath));
      da.SetData(_pathOut, result);
    }

    public override void AddedToDocument(GH_Document document)
    {
      base.AddedToDocument(document);
      if(document != null) document.FilePathChanged += DocumentOnFilePathChanged;
    }

    private void DocumentOnFilePathChanged(object o, GH_DocFilePathEventArgs ghDocFilePathEventArgs)
    {
      ExpireSolution(true);
    }

    public override void RemovedFromDocument(GH_Document document)
    {
      base.RemovedFromDocument(document);
      if (document != null) document.FilePathChanged -= DocumentOnFilePathChanged;
    }

    /// <summary>
    /// Provides an Icon for every component that will be visible in the User Interface.
    /// Icons need to be 24x24 pixels.
    /// </summary>
    protected override System.Drawing.Bitmap Icon => Resources.Icon;

    /// <summary>
    /// Each component must have a unique Guid to identify it. 
    /// It is vital this Guid doesn't change otherwise old ghx files 
    /// that use the old ID will partially fail during loading.
    /// </summary>
    public override Guid ComponentGuid => new Guid("c504a005-7b82-4554-954b-60c7c8607934");
  }
}
