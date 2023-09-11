# OnlineShop

# How To Installer

- Steps
    * ``` git clone  https://github.com/samedayan/online-shop.git ```

    * ``` docker compose -f {path}/online-shop/docker-compose_onlineShop.yml up  ```

      > The ``` docker-compose_sonarqube.yml``` file is left for information purposes.
    * ``` {path}/online-shop/etc/online-shop.sql ``` The sql commands in the file must be run sequentially. When changes are made that affect any database in the project, the query should be added to this file.
    * 