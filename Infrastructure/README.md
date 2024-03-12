# About this reposity

This repo is based of off the [kind-playground](https://github.com/eddycharly/kind-playground) repository by Eddy Charly.
I updated the scripts they wrote, fixed a bunch of issues I ran into, and then adjusted it to work my project.

# Stuff so this works

## Make sure you have required stuff

- Docker: https://docs.docker.com/get-docker/
- kubectl: https://kubernetes.io/docs/tasks/tools/
- Helm: https://helm.sh/docs/intro/install/
- Kind: https://kind.sigs.k8s.io/
- Terraform: https://developer.hashicorp.com/terraform/tutorials/aws-get-started/install-cli

## Install dnsmasq

Install it

```shell
sudo apt-get install dnsmasq
```

and create a `dnsmasq.conf` file in `/etc/dnsmasq.d/` with the following content:

```
bind-interfaces
listen-address=127.0.0.1
server=8.8.8.8
server=8.8.4.4
conf-dir=/etc/dnsmasq.d/,*.conf
```

and restart the service with

```shell
sudo systemctl restart dnsmasq
```

Then set dnsmasq als your dns by adding the following to `/etc/systemd/resolved.conf`

```
DNS=127.0.0.1
DNSStubListener=no
```

and restart the resolver with

```shell
sudo systemctl restart systemd-resolved
```

## Increase file watch capacity

Increase system file watch capacity if nodes fail to start with "too many files open":

```shell
sudo sysctl fs.inotify.max_user_instances=1280
sudo sysctl fs.inotify.max_user_watches=655360
```

## Finally, run it

```shell
./_bootstrap.sh --kube-prometheus-stack
```

```shell
./rekeningrijden.sh
```

## In case stuff doesn't work

Try deleting all:
1. Kubernetes resources
```shell
kubectl delete all --all
```

2. Dangling docker volumes
```shell
docker volume prune
```
3. Reinstalling the cluster by running the bootstrap script