<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
				xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html"/>

	<xsl:template match="/">
		<html>
			<body topmargin="150"
				  style ="background-image: url(a6.jpg); 
				  background-position: center;  
				  background-repeat: no-repeat;  
				  background-size: cover;">
				<table border ="4px inset grey"
					   align ="center"
					   width="50%"
					   height="100%"
					   bordercolor ="Grey">
					<tbody align ="center">
						<TR bgcolor ="LightSteelBlue">
							<th>Faculty</th>
							<th>Group</th>
							<th>Years of studying</th>
							<th>Name</th>
							<th>Room</th>
							<th>Floor</th>
							<th>Number</th>
						</TR>
						<xsl:for-each select="DataBaseTable/faculty">
							<tr bgcolor ="CornflowerBlue">
								<td>
									<xsl:value-of select="@FCNAME"/>
								</td>
								<td>
									<xsl:value-of select="group/@GROUPNAME"/>
								</td>
								<td>
									<xsl:value-of select="group/section/@COURSE"/>
								</td>
								<td>
									<xsl:value-of select="group/section/@NAME"/>
								</td>
								<td>
									<xsl:value-of select="group/section/@ROOM"/>
								</td>
								<td>
									<xsl:value-of select="group/section/@FLOOR"/>
								</td>
								<td>
									<xsl:value-of select="group/section/@NUMBER"/>
								</td>
							</tr>
						</xsl:for-each>
					</tbody>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
