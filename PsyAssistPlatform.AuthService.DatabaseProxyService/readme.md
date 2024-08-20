# Запуск сервиса на удаленной виртуальной машине

## Шаги для создания службы:

1. Создайте файл службы:

Создайте новый файл в `/etc/systemd/system:`

```
sudo nano /etc/systemd/system/psyassist.service
```

2.Добавьте конфигурацию службы:

 ```
 [Unit]
Description=PsyAssist Platform Auth Service
After=network.target

[Service]
WorkingDirectory=/home/alexey/DatabaseProxyService/publish
ExecStart=/usr/bin/dotnet /home/alexey/DatabaseProxyService/publish/PsyAssistPlatform.AuthService.DatabaseProxyService.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=psyassist
User=alexey
Environment=ASPNETCORE_ENVIRONMENT=Production

[Install]
WantedBy=multi-user.target
```

3. Перезагрузите службы и запустите вашу службу:

```
sudo systemctl daemon-reload
sudo systemctl start psyassist
sudo systemctl enable psyassist

```
Проверка статуса службы:

```
sudo systemctl status psyassist
```

## Шаги для обновление службы

#### Шаг 1. Остановить службу 
```
sudo systemctl stop psyassist
```
Проверить статус
```
sudo systemctl status psyassist
```
Служба должна быть в состоянии "inactive" или "dead".

#### Шаг 2. Обновление файлов сервиса
1. Скопировать файлы сборки на машину. Пример команды
```
scp -r /path/to/local/publish/* user@remote_host:/home/alexey/DatabaseProxyService/publish/
```

#### Шаг 3: Перезапуск службы

1. После копирования новых файлов перезапустите службу:

```
sudo systemctl start psyassist
```
Проверьте статус службы, чтобы убедиться, что она успешно запустилась:
```
sudo systemctl status psyassist
```

Если нужно, просмотрите логи службы, чтобы убедиться в отсутствии ошибок:
```
sudo journalctl -u psyassist -f
```

#### Шаг 4: Проверка работы сервиса

Воспользоваться API