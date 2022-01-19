UNAME_S = $(shell uname -s)
UNAME_M = $(shell uname -m)

ifeq ($(UNAME_S),Darwin)
  SYSTEM = osx
endif
ifeq ($(UNAME_S),Linux)
  SYSTEM = linux
endif

ifeq ($(UNAME_M),x86_64)
  ARCH=x64
endif
ifeq ($(UNAME_M),arm64)
  ARCH=arm64
endif

RUNTIME=$(SYSTEM)-$(ARCH)
DOTNETVER=6.0

all:
	dotnet publish --runtime $(RUNTIME) --self-contained --configuration Release
	ls -lh bin/Release/net$(DOTNETVER)/$(RUNTIME)/publish/WalletWasabi.Middleware

run:
	./bin/Release/net$(DOTNETVER)/$(RUNTIME)/publish/WalletWasabi.Middleware

clean:
	rm -rf bin/ obj/
