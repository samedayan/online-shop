# OnlineShop

# How to Installer

- Steps
    * ``` git clone  https://github.com/samedayan/online-shop.git ```

    * ``` docker compose -f {path}/online-shop/docker-compose_onlineShop.yml up  ```

      > The ``` docker-compose_sonarqube.yml``` file is left for information purposes.
    * ``` {path}/online-shop/etc/online-shop.sql ``` The sql commands in the file must be run sequentially. When changes are made that affect any database in the project, the query should be added to this file.

    
## Customers Database

|User            |                               |                             |
|----------------|-------------------------------|-----------------------------|
| PK             |`Id               `            |int                          |
|                |`Name             `            |string                       |
|                |`LastName                     `|string                       |
|                |`Phone                        `|string                       |
|                |`Email                        `|string                       |
|                |`Address                      `|string                       |
|                |`CreatedDate                  `|DateTime                     |
|                |`LastModifiedDate             `|DateTime?                    |

## Products Database

|Product         |                               |                             |
|----------------|-------------------------------|-----------------------------|
| PK             |`Id               `            |int                          |
|                |`Code             `            |string                       |
|                |`Name                         `|string                       |
|                |`CategoryId                   `|int                          |
|                |`Description                  `|string                       |
|                |`ImagePath                    `|string                       |
|                |`CreatedDate                  `|DateTime                     |
|                |`LastModifiedDate             `|DateTime?                    |

|ProductProperties|                              |                             |
|----------------|-------------------------------|-----------------------------|
| PK             |`Id               `            |int                          |
|                |`ProductId        `            |int                          |
|                |`ColorId                      `|int                          |
|                |`Description                  `|string                       |
|                |`CreatedDate                  `|DateTime                     |
|                |`LastModifiedDate             `|DateTime?                    |

|ProductPrice    |                               |                             |
|----------------|-------------------------------|-----------------------------|
| PK             |`Id               `            |int                          |
|                |`ProductId        `            |int                          |
|                |`OldPrice                     `|numeric                      |
|                |`NewPrice                     `|numeric                      |
|                |`IsActive                     `|boolean                      |
|                |`CreatedDate                  `|DateTime                     |
|                |`LastModifiedDate             `|DateTime?                    |

|ProductStock    |                               |                             |
|----------------|-------------------------------|-----------------------------|
| PK             |`Id               `            |int                          |
|                |`ProductId        `            |int                          |
|                |`Quantity                     `|int                          |
|                |`IsActive                     `|boolean                      |
|                |`CreatedDate                  `|DateTime                     |
|                |`LastModifiedDate             `|DateTime?                    |

## Orders Database

|Order           |                               |                             |
|----------------|-------------------------------|-----------------------------|
| PK             |`Id               `            |int                          |
|                |`CustomerId       `            |int                          |
|                |`StatusId                     `|int                          |
|                |`Description                  `|string                       |
|                |`CreatedDate                  `|DateTime                     |
|                |`LastModifiedDate             `|DateTime?                    |

|OrderProduct    |                               |                             |
|----------------|-------------------------------|-----------------------------|
| PK             |`Id               `            |int                          |
|                |`OrderId          `            |int                          |
|                |`ProductId                    `|int                          |
|                |`Quantity                     `|int                          |
|                |`Description                  `|string                       |
|                |`CreatedDate                  `|DateTime                     |
|                |`LastModifiedDate             `|DateTime?                    |

## Services

[![image](https://www.linkpicture.com/q/OnlineShop.drawio.png)](https://www.linkpicture.com/view.php?img=LPic64ff07a144f7e1719641086)

[![image](https://www.linkpicture.com/q/Screenshot-2023-09-11-232429_1.png)](https://www.linkpicture.com/view.php?img=LPic64ff78c3e0054465738220)