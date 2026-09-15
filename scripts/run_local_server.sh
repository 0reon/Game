#!/bin/bash
# Простой скрипт для локального деплоя WebGL билда на временный HTTP сервер
# Использует Python http.server (нужен Python 3)

BUILD_DIR="Builds/WebGL"
PORT=8000
if [ ! -d "$BUILD_DIR" ]; then
  echo "Не найдена папка билда: $BUILD_DIR"
  exit 1
fi

echo "Сервер будет доступен по http://localhost:$PORT"
cd "$BUILD_DIR"
python3 -m http.server $PORT
