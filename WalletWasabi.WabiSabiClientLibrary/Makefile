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

BINARY=bin/Release/net$(DOTNETVER)/$(RUNTIME)/publish/WalletWasabi.WabiSabiClientLibrary
OPTS=-p:PublishSingleFile=True -p:PublishTrimmed=True

all: compile

ifeq (,$(wildcard /etc/NIXOS))
INTERPRETER=$(shell cat ${NIX_CC}/nix-support/dynamic-linker)
all: patchelf
endif

compile:
	dotnet publish --runtime $(RUNTIME) --self-contained --configuration Release $(OPTS)
	ls -lh $(BINARY)

patchelf:
	nix-shell -p patchelf --run 'patchelf --set-interpreter "$(INTERPRETER)" "$(BINARY)"'

run:
	$(BINARY)

clean:
	rm -rf bin/ obj/
