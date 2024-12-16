using System.Data;

using Lib.DBAccess.Helpers;

using Oracle.ManagedDataAccess.Client;

namespace Lib.DBAccess.Builder;

public class OracleParameterBuilder
{
    private readonly List<OracleParameter> _oracleParameters = [];

    private OracleParameterBuilder()
    {
        _oracleParameters.Add(new OracleParameter(DefaultConstants.ErrorCodeParameter, OracleDbType.Int32)
        {
            Direction = ParameterDirection.Output,
        });
        _oracleParameters.Add(new OracleParameter(DefaultConstants.ErrorMessageParameter, OracleDbType.Varchar2, 200)
        {
            Direction = ParameterDirection.Output,
        });
    }

    internal static OracleParameterBuilder Create()
    {
        return new OracleParameterBuilder();
    }

    public OracleParameterBuilder AddOutputParameter()
    {
        _oracleParameters.Add(new OracleParameter(DefaultConstants.OutputParameter, OracleDbType.Int32)
        {
            Direction = ParameterDirection.Output,
        });

        return this;
    }

    public OracleParameterBuilder AddOutParameter(string parameterName, OracleDbType dbType)
    {
        _oracleParameters.Add(new OracleParameter(parameterName, dbType)
        {
            Direction = ParameterDirection.Output,
        });

        return this;
    }

    public OracleParameterBuilder AddIntParameter(string parameterName, int? value)
    {
        _oracleParameters.Add(new OracleParameter(parameterName, OracleDbType.Int32)
        {
            Value = value,
        });

        return this;
    }

    public OracleParameterBuilder AddLongParameter(string parameterName, long? value)
    {
        _oracleParameters.Add(new OracleParameter(parameterName, OracleDbType.Int64)
        {
            Value = value,
        });

        return this;
    }

    public OracleParameterBuilder AddDoubleParameter(string parameterName, double? value)
    {
        _oracleParameters.Add(new OracleParameter(parameterName, OracleDbType.Double)
        {
            Value = value,
        });

        return this;
    }

    public OracleParameterBuilder AddDecimalParameter(string parameterName, decimal? value)
    {
        _oracleParameters.Add(new OracleParameter(parameterName, OracleDbType.Decimal)
        {
            Value = value,
        });

        return this;
    }

    public OracleParameterBuilder AddVarchar2Parameter(string parameterName, string? value)
    {
        _oracleParameters.Add(new OracleParameter(parameterName, OracleDbType.Varchar2)
        {
            Value = value,
        });

        return this;
    }

    public OracleParameterBuilder AddNVarchar2Parameter(string parameterName, string? value)
    {
        _oracleParameters.Add(new OracleParameter(parameterName, OracleDbType.NVarchar2)
        {
            Value = value,
        });

        return this;
    }

    public OracleParameterBuilder AddDateTimeParameter(string parameterName, DateTime? value)
    {
        _oracleParameters.Add(new OracleParameter(parameterName, OracleDbType.Date)
        {
            Value = value is null || value == DateTime.MinValue || value == DateTime.MaxValue
                ? DBNull.Value
                : value,
        });

        return this;
    }

    public OracleParameterBuilder AddBlobParameter(string parameterName, byte[]? value)
    {
        _oracleParameters.Add(new OracleParameter(parameterName, OracleDbType.Blob)
        {
            Value = value,
        });

        return this;
    }

    /// <summary>
    /// If value is null then it sets the parameter value as null
    /// </summary>
    /// <param name="parameterName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public OracleParameterBuilder AddCharFromBoolParameter(string parameterName, bool? value)
    {
        _oracleParameters.Add(new OracleParameter(parameterName, OracleDbType.Char)
        {
            Value = value is null ? null : value == true ? 'Y' : 'N',
        });

        return this;
    }

    public OracleParameterBuilder AddIntListParameter(string parameterName, IList<int> value)
    {
        var parameter = new OracleParameter(parameterName, OracleDbType.Int32, ParameterDirection.Input)
        {
            CollectionType = OracleCollectionType.PLSQLAssociativeArray,
            Value = value.ToArray(),
            Size = value.Count,
        };
        _oracleParameters.Add(parameter);

        return this;
    }

    public OracleParameterBuilder AddLongListParameter(string parameterName, IList<long> value)
    {
        var parameter = new OracleParameter(parameterName, OracleDbType.Int64, ParameterDirection.Input)
        {
            CollectionType = OracleCollectionType.PLSQLAssociativeArray,
            Value = value.ToArray(),
            Size = value.Count,
        };
        _oracleParameters.Add(parameter);

        return this;
    }

    public OracleParameterBuilder AddDecimalListParameter(string parameterName, IList<decimal> value)
    {
        var parameter = new OracleParameter(parameterName, OracleDbType.Decimal, ParameterDirection.Input)
        {
            CollectionType = OracleCollectionType.PLSQLAssociativeArray,
            Value = value.ToArray(),
            Size = value.Count,
        };
        _oracleParameters.Add(parameter);

        return this;
    }

    public IEnumerable<OracleParameter> Build()
    {
        return _oracleParameters;
    }

    public IEnumerable<OracleParameter> BuildForQuery()
    {
        _oracleParameters.Add(new OracleParameter(DefaultConstants.CursorParameter, OracleDbType.RefCursor)
        {
            Direction = ParameterDirection.Output,
        });

        return _oracleParameters;
    }
}
