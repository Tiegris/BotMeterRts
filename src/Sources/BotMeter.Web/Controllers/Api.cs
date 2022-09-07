using BotMeter.Core;
using BotMeter.Core.Types;
using Microsoft.AspNetCore.Mvc;
using Vector = System.Numerics.Vector;

namespace BotMeter.Web.Controllers;

[ApiController]
public class Api : ControllerBase
{
    [HttpGet("/svg")]
    public string GetSvg()
    {
        var path = @"F:\TELAPO\StereoGraph\jegyzokonyvek\proghazihoz.csv";

        CoordinateSystem cs;
        using (var reader = new DataReader(path))
        {
            MeasuredData polaris = reader.Next();
            cs = new CoordinateSystem(polaris.Star.Vector, polaris.DateTime);

            while (!reader.EndOfStream)
                cs.Add(reader.Next());
        }

        Projection p = new Projection(cs);
        p.MirrorOnX();

        return p.ToSvg();
    }
    
    
}