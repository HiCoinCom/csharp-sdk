using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ChainUpCustody.Api.Models.Mpc;
using ChainUpCustody.Crypto;

namespace ChainUpCustody.Utilities
{
  /// <summary>
  /// MPC transaction signing utility
  /// </summary>
  public static class MpcSignUtil
  {
    /// <summary>
    /// Get MPC signature for the given parameters using DataCrypto
    /// </summary>
    /// <param name="signParams">Parameters to sign</param>
    /// <param name="dataCrypto">Data crypto instance</param>
    /// <returns>Signature string</returns>
    public static string GetMpcSign(Dictionary<string, string> signParams, IDataCrypto dataCrypto)
    {
      var signData = ParamsSort(signParams);
      if (string.IsNullOrEmpty(signData))
      {
        return string.Empty;
      }
      var md5Hash = GetMd5(signData);
      return dataCrypto.Sign(md5Hash);
    }

    /// <summary>
    /// Get withdrawal signature using DataCrypto
    /// </summary>
    /// <param name="withdrawArgs">Withdraw args</param>
    /// <param name="dataCrypto">Data crypto instance</param>
    /// <returns>Signature string</returns>
    public static string GetWithdrawSign(MpcWithdrawArgs withdrawArgs, IDataCrypto dataCrypto)
    {
      if (withdrawArgs == null)
      {
        return string.Empty;
      }
      var signParamsMap = GetWithdrawSignParams(withdrawArgs);
      var signData = ParamsSort(signParamsMap);
      var md5Hash = GetMd5(signData);
      return dataCrypto.Sign(md5Hash);
    }

    /// <summary>
    /// Get Web3 transaction signature using DataCrypto
    /// </summary>
    /// <param name="createWeb3Args">Create Web3 args</param>
    /// <param name="dataCrypto">Data crypto instance</param>
    /// <returns>Signature string</returns>
    public static string GetWeb3Sign(CreateWeb3Args createWeb3Args, IDataCrypto dataCrypto)
    {
      if (createWeb3Args == null)
      {
        return string.Empty;
      }
      var signParamsMap = GetWeb3SignParams(createWeb3Args);
      var signData = ParamsSort(signParamsMap);
      var md5Hash = GetMd5(signData);
      return dataCrypto.Sign(md5Hash);
    }

    /// <summary>
    /// Get withdrawal signature parameters
    /// </summary>
    /// <param name="withdrawArgs">Withdraw args</param>
    /// <returns>Parameters map</returns>
    public static Dictionary<string, string> GetWithdrawSignParams(MpcWithdrawArgs withdrawArgs)
    {
      var signParamsMap = new Dictionary<string, string>
            {
                { "request_id", withdrawArgs.RequestId ?? string.Empty },
                { "sub_wallet_id", withdrawArgs.SubWalletId?.ToString() ?? string.Empty },
                { "symbol", withdrawArgs.Symbol ?? string.Empty },
                { "address_to", withdrawArgs.AddressTo ?? string.Empty },
                { "amount", withdrawArgs.Amount ?? string.Empty },
                { "memo", withdrawArgs.Memo ?? string.Empty },
                { "outputs", withdrawArgs.Outputs ?? string.Empty }
            };
      return signParamsMap;
    }

    /// <summary>
    /// Get Web3 signature parameters
    /// </summary>
    /// <param name="createWeb3Args">Create Web3 args</param>
    /// <returns>Parameters map</returns>
    public static Dictionary<string, string> GetWeb3SignParams(CreateWeb3Args createWeb3Args)
    {
      var signParamsMap = new Dictionary<string, string>
            {
                { "request_id", createWeb3Args.RequestId ?? string.Empty },
                { "sub_wallet_id", createWeb3Args.SubWalletId?.ToString() ?? string.Empty },
                { "main_chain_symbol", createWeb3Args.MainChainSymbol ?? string.Empty },
                { "interactive_contract", createWeb3Args.InteractiveContract ?? string.Empty },
                { "amount", createWeb3Args.Amount ?? string.Empty },
                { "input_data", createWeb3Args.InputData ?? string.Empty }
            };
      return signParamsMap;
    }

    /// <summary>
    /// Sort parameters in ASCII order and format as k1=v1&amp;k2=v2
    /// Parameters with empty values are not included in the signature
    /// Reference: https://github.com/HiCoinCom/rust-sdk/blob/main/src/mpc/sign_util.rs
    /// </summary>
    /// <param name="parameters">Parameters to sort</param>
    /// <returns>Sorted parameter string (lowercase)</returns>
    public static string ParamsSort(Dictionary<string, string> parameters)
    {
      if (parameters == null || parameters.Count == 0)
      {
        return string.Empty;
      }

      // Sort by key in ASCII order and filter out empty values
      var sortedParams = parameters
          .Where(kvp => !string.IsNullOrEmpty(kvp.Value))
          .OrderBy(kvp => kvp.Key, StringComparer.Ordinal)
          .ToList();

      if (sortedParams.Count == 0)
      {
        return string.Empty;
      }

      // Format as k1=v1&k2=v2 (with & separator) and convert to lowercase
      // Reference: https://github.com/HiCoinCom/rust-sdk/blob/main/src/mpc/sign_util.rs#L91
      var result = string.Join("&", sortedParams.Select(kvp => $"{kvp.Key}={FormatValue(kvp.Value)}"));
      return result.ToLowerInvariant();
    }

    /// <summary>
    /// Format value - remove trailing zeros for decimal numbers
    /// </summary>
    private static string FormatValue(string value)
    {
      if (string.IsNullOrEmpty(value))
      {
        return value;
      }

      // Try to parse as decimal and remove trailing zeros
      if (decimal.TryParse(value, out var decimalValue))
      {
        // Only format if it contains a decimal point
        if (value.Contains('.'))
        {
          return decimalValue.ToString("G29");
        }
      }

      return value;
    }

    /// <summary>
    /// Get MD5 hash of the input string
    /// </summary>
    /// <param name="input">Input string</param>
    /// <returns>MD5 hash in lowercase hex</returns>
    public static string GetMd5(string input)
    {
      if (string.IsNullOrEmpty(input))
      {
        return string.Empty;
      }

      using var md5 = MD5.Create();
      var inputBytes = Encoding.UTF8.GetBytes(input);
      var hashBytes = md5.ComputeHash(inputBytes);

      var sb = new StringBuilder();
      foreach (var b in hashBytes)
      {
        sb.Append(b.ToString("x2"));
      }
      return sb.ToString();
    }
  }
}
