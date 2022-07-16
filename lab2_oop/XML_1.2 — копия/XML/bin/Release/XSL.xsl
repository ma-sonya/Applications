<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
				xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html"/>

	<xsl:template match="/">
		<html>
			<body topmargin="150"
				  style ="background-image: url(D:/csc.jpg); 
				  background-position: center;  
				  background-repeat: no-repeat;  
				  background-size: cover;">
				<table border ="4px inset grey"
					   align ="center"
					   width="50%"
					   height="100%"					   
					   bordercolor ="Black">
					<tbody align ="center">
						<TR bgcolor ="#F1C40F">
							<th>Faculty</th>
							<th>Department</th>
							<th>Section</th>
							<th>Name</th>
							<th>Cathedra</th>
							<th>Audience</th>
							<th>Curriculum</th>
							<th>Composition of students</th>
						</TR>
						<xsl:for-each select="DataBaseTable/faculty">
							<tr bgcolor ="#F1C40F">
								<td>
									<xsl:value-of select="@FCNAME"/>
								</td>
								<td>
									<xsl:value-of select="department/@DEPNAME"/>
								</td>
								<td>
									<xsl:value-of select="department/section/@SECNAME"/>
								</td>
								<td>
									<xsl:value-of select="department/section/@NAME"/>
								</td>
								<td>
									<xsl:value-of select="department/section/@CATHEDRA"/>
								</td>
								<td>
									<xsl:value-of select="department/section/@AUDIENCE"/>
								</td>
								<td>
									<xsl:value-of select="department/section/@CURRICULUM"/>
								</td>
								<td>
									<xsl:value-of select="department/section/@STUDENTS"/>
								</td>
							</tr>
						</xsl:for-each>
					</tbody>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
