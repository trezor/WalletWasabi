with import <nixpkgs> { };

stdenv.mkDerivation rec {
  name = "wabisabi-env";

  buildInputs = [ dotnet-sdk_7 zlib ];

  # for WalletWasabi.WabiSabiClientLibrary
  LD_LIBRARY_PATH = "${gcc}/lib:${openssl.out}/lib:${zlib}/lib:${stdenv.cc.cc.lib}/lib:${icu}/lib";
}
