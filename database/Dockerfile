FROM microsoft/mssql-server-linux
COPY entrypoint.sh entrypoint.sh
COPY SqlCmdStartup.sh SqlCmdStartup.sh
COPY SqlCmdScript.sql SqlCmdScript.sql
RUN chmod +x ./SqlCmdStartup.sh
CMD /bin/bash ./entrypoint.sh