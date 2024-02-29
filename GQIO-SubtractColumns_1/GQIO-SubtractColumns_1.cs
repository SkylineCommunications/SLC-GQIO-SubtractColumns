using System;
using Skyline.DataMiner.Analytics.GenericInterface;

[GQIMetaData(Name = "Subtract")]
public class AgeCalculator : IGQIColumnOperator, IGQIRowOperator, IGQIInputArguments
{
    private readonly GQIColumnDropdownArgument _Column1Argument = new GQIColumnDropdownArgument("Column 1") { IsRequired = true, Types = new GQIColumnType[] { GQIColumnType.Double, GQIColumnType.Int } };
    private readonly GQIColumnDropdownArgument _Column2Argument = new GQIColumnDropdownArgument("Column 2") { IsRequired = true, Types = new GQIColumnType[] { GQIColumnType.Double, GQIColumnType.Int } };
    private readonly GQIStringArgument _NameArgument = new GQIStringArgument("Column Name") { IsRequired = true };
    private GQIDoubleColumn _ResultColumn;
    private GQIEditableDoubleColumn _Column1;
    private GQIEditableDoubleColumn _Column2;
    private string _Name;

    public GQIArgument[] GetInputArguments()
    {
        return new GQIArgument[] { _Column1Argument, _Column2Argument, _NameArgument };
    }

    public OnArgumentsProcessedOutputArgs OnArgumentsProcessed(OnArgumentsProcessedInputArgs args)
    {
        _Column1 = args.GetArgumentValue(_Column1Argument) as GQIEditableDoubleColumn;
        _Column2 = args.GetArgumentValue(_Column2Argument) as GQIEditableDoubleColumn;
        _Name = args.GetArgumentValue(_NameArgument);
        return new OnArgumentsProcessedOutputArgs();
    }

    public void HandleRow(GQIEditableRow row)
    {
        if (row.TryGetValue(_Column1, out var value1) && row.TryGetValue(_Column2, out var value2))
        {
            row.SetValue(_ResultColumn, value1 - value2);
        }
        else
        {
            row.SetValue(_ResultColumn, 0);
        }
    }

    public void HandleColumns(GQIEditableHeader header)
    {
        _ResultColumn = new GQIDoubleColumn(_Name);
        header.AddColumns(_ResultColumn);
    }
}