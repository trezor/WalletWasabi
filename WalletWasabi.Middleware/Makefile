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

BINARY=bin/Release/net$(DOTNETVER)/$(RUNTIME)/publish/WalletWasabi.Middleware
OPTS=-p:PublishSingleFile=True -p:PublishTrimmed=True

all:
	dotnet publish --runtime $(RUNTIME) --self-contained --configuration Release $(OPTS)
	ls -lh $(BINARY)

run:
	$(BINARY)

clean:
	rm -rf bin/ obj/
