Пришлось поставить nunit3-console (качнул с офсервера)

после установки прописать путь к бинарнику в перенной path окружения (у меня это C:\Program Files (x86)\NUnit.org\nunit-console)

после сборки проекта в папке bin/Debug будет файл с именем проекта.dll ( у меня это  addressbook-web-tests-unit-tests.dll)

запускать соответственно

nunit3-console addressbook-web-tests-unit-tests.dll

если где нужно прописать полные пути....

как стартовать отдельные тесты я хз пока...