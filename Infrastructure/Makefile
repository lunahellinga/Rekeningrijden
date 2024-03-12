source:
	source ~/.bashrc

prep: source
	sudo sysctl fs.inotify.max_user_instances=1280
	sudo sysctl fs.inotify.max_user_watches=655360

run: source
	./_bootstrap.sh --kube-prometheus-stack

helm: source
	cd ../KwetterWeb/kwetter-web/
	helm install kwetter-web .

rabbitmq:
	docker run -p 15672:15672 -p 5672:5672 masstransit/rabbitmq