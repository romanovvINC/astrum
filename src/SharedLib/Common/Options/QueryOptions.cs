﻿namespace Astrum.SharedLib.Common.Options;

public class QueryOptions
{
    private string _orderBy = string.Empty;

    private int? _pageNo = 1;
    private int? _pageSize = 10;

    public int PageSize
    {
        get => _pageSize.Value;
        set => _pageSize = value;
    }

    public int PageNo
    {
        get =>
            _pageNo.GetValueOrDefault(0) <= 1
                ? 1
                : _pageNo.Value;
        set => _pageNo = value;
    }

    public int Skip
    {
        get;
        set;
    }

    public string SearchTerm { get; set; }

    public string OrderBy
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(_orderBy))
                return IsAscending
                    ? $"{_orderBy} asc"
                    : $"{_orderBy} desc";
            return _orderBy;
        }
        set => _orderBy = value;
    }

    public bool IsAscending { get; set; }
}