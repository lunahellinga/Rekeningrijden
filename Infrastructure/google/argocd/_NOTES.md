1. Run
    ```shell
    mv setup/repo-empty.yaml setup/repo.yaml
    ```
2. Set user and password in repo.yaml
3. Apply the repo.yaml with the before trying to add other argocd apps
    ```shell
    kubectl apply -f setup/repo.yaml
    ```
4. 