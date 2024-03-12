# General usage makefile with various commands

mkfile_path := $(abspath $(lastword $(MAKEFILE_LIST)))
mkfile_dir := $(dir $(mkfile_path))

structurizr-install:
	docker pull structurizr/lite

structurizr-setup:
	docker run -it --rm -p 8080:8080 -v $(mkfile_dir):/usr/local/structurizr structurizr/lite

structurizr-run:
	sleep 10
	-open http://localhost:8080

structurizr-all: structurizr-setup structurizr-run
	echo "Structurizr shut down"

structurizr:
	make -j 2 structurizr-all