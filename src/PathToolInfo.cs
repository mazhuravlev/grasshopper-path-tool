using System;
using System.Drawing;
using Grasshopper.Kernel;
using TplTest.Properties;

namespace TplTest
{
  public class PathToolInfo : GH_AssemblyInfo
  {
    public override string Name => "PathTool";

    public override Bitmap Icon => Resources.Icon;

    public override string Description => "Create file path relative to GH document file";

    public override Guid Id => new Guid("c3292124-cc4a-4543-85d3-e85b2e79d820");

    public override string AuthorName => "Mikhail Zhuravlev";

    public override string AuthorContact => "m.zhuravlev61@gmail.com";
  }
}
